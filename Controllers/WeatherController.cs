using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;
using WeatherAPI.Repositories;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ICacheRepository _cache;

        public WeatherController(IWeatherService weatherService, ICacheRepository cache)
        {
            _weatherService = weatherService;
            _cache = cache;
        }

        [HttpGet("clima/{ciudad}")]
        public async Task<ActionResult<WeatherResponse>> GetClima(string ciudad)
        {
            try
            {
                var result = await _weatherService.GetCurrentWeatherAsync(ciudad);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { error = "Error al obtener datos del clima", detalle = ex.Message });
            }
        }

        [HttpGet("pronostico/{ciudad}")]
        public async Task<ActionResult<ForecastResponse>> GetPronostico(string ciudad)
        {
            try
            {
                var result = await _weatherService.GetForecastAsync(ciudad);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { error = "Error al obtener pronóstico", detalle = ex.Message });
            }
        }

        [HttpPost("comparar")]
        public async Task<ActionResult<CompareResponse>> CompararCiudades([FromBody] CompareRequest request)
        {
            if (request.Ciudades == null || request.Ciudades.Count < 2)
            {
                return BadRequest(new { error = "Debes proporcionar al menos 2 ciudades" });
            }

            try
            {
                var result = await _weatherService.CompareWeatherAsync(request.Ciudades);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al comparar ciudades", detalle = ex.Message });
            }
        }

        [HttpDelete("cache")]
        public ActionResult ClearCache()
        {
            _cache.Clear();
            return Ok(new { mensaje = "Caché limpiado exitosamente" });
        }

        [HttpGet("health")]
        public ActionResult<object> HealthCheck()
        {
            return Ok(new
            {
                status = "OK",
                timestamp = DateTime.UtcNow,
                cacheSize = _cache.GetCacheSize()
            });
        }
    }
}