using System.Text.Json;
using WeatherAPI.Models;
using WeatherAPI.Models.External;
using WeatherAPI.Repositories;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheRepository _cache;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public WeatherService(
            HttpClient httpClient, 
            ICacheRepository cache, 
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _cache = cache;
            _configuration = configuration;
            _apiKey = _configuration["OpenWeatherMap:ApiKey"] ?? "TU_API_KEY_AQUI";
            _baseUrl = "https://api.openweathermap.org/data/2.5";
        }

        public async Task<WeatherResponse> GetCurrentWeatherAsync(string ciudad)
        {
            var cacheKey = $"clima_{ciudad.ToLower()}";
            
            var cached = _cache.Get<WeatherResponse>(cacheKey);
            if (cached != null)
            {
                cached.FromCache = true;
                return cached;
            }

            var url = $"{_baseUrl}/weather?q={ciudad}&appid={_apiKey}&units=metric&lang=es";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener clima: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var externalData = JsonSerializer.Deserialize<OpenWeatherMapResponse>(content);

            if (externalData == null)
            {
                throw new InvalidOperationException("Error al deserializar respuesta");
            }

            var result = new WeatherResponse
            {
                Ciudad = externalData.Name,
                Pais = externalData.Sys.Country,
                Temperatura = (int)Math.Round(externalData.Main.Temp),
                SensacionTermica = (int)Math.Round(externalData.Main.Feels_Like),
                Descripcion = externalData.Weather.FirstOrDefault()?.Description ?? "",
                Humedad = externalData.Main.Humidity,
                Viento = externalData.Wind.Speed,
                Timestamp = DateTime.UtcNow,
                FromCache = false
            };

            _cache.Set(cacheKey, result);
            
            return result;
        }

        public async Task<ForecastResponse> GetForecastAsync(string ciudad)
        {
            var cacheKey = $"pronostico_{ciudad.ToLower()}";
            
            var cached = _cache.Get<ForecastResponse>(cacheKey);
            if (cached != null)
            {
                cached.FromCache = true;
                return cached;
            }

            var url = $"{_baseUrl}/forecast?q={ciudad}&appid={_apiKey}&units=metric&lang=es";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener pronóstico: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var externalData = JsonSerializer.Deserialize<OpenWeatherMapForecastResponse>(content);

            if (externalData == null)
            {
                throw new InvalidOperationException("Error al deserializar respuesta");
            }

            var groupedByDay = externalData.List
                .Take(24)
                .GroupBy(item => item.Dt_Txt.Split(' ')[0])
                .Select(g => new DayForecast
                {
                    Fecha = g.Key,
                    TempPromedio = (int)Math.Round(g.Average(x => x.Main.Temp)),
                    TempMax = (int)Math.Round(g.Max(x => x.Main.Temp)),
                    TempMin = (int)Math.Round(g.Min(x => x.Main.Temp)),
                    Descripcion = g.First().Weather.FirstOrDefault()?.Description ?? ""
                })
                .ToList();

            var result = new ForecastResponse
            {
                Ciudad = externalData.City.Name,
                Pais = externalData.City.Country,
                Dias = groupedByDay,
                FromCache = false
            };

            _cache.Set(cacheKey, result);
            
            return result;
        }

        public async Task<CompareResponse> CompareWeatherAsync(List<string> ciudades)
        {
            var tasks = ciudades.Select(ciudad => GetCurrentWeatherAsync(ciudad));
            var resultados = await Task.WhenAll(tasks);

            var comparacion = resultados.Select(r => new CityComparison
            {
                Ciudad = r.Ciudad,
                Temperatura = r.Temperatura,
                Descripcion = r.Descripcion
            }).ToList();

            var masCalida = comparacion.OrderByDescending(c => c.Temperatura).First();
            var masFria = comparacion.OrderBy(c => c.Temperatura).First();

            return new CompareResponse
            {
                Comparacion = comparacion,
                MasCalida = masCalida,
                MasFria = masFria,
                DiferenciaTemperatura = masCalida.Temperatura - masFria.Temperatura
            };
        }
    }
}