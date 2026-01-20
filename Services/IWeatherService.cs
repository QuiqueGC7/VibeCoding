using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetCurrentWeatherAsync(string ciudad);
        Task<ForecastResponse> GetForecastAsync(string ciudad);
        Task<CompareResponse> CompareWeatherAsync(List<string> ciudades);
    }
}