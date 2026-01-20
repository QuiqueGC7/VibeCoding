namespace WeatherAPI.Models
{
    public class ForecastResponse
    {
        public string Ciudad { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public List<DayForecast> Dias { get; set; } = new();
        public bool FromCache { get; set; }
    }

    public class DayForecast
    {
        public string Fecha { get; set; } = string.Empty;
        public int TempPromedio { get; set; }
        public int TempMax { get; set; }
        public int TempMin { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}