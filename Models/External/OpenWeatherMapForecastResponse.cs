namespace WeatherAPI.Models.External
{
    public class OpenWeatherMapForecastResponse
    {
        public City City { get; set; } = new();
        public List<ForecastItem> List { get; set; } = new();
    }

    public class City
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public class ForecastItem
    {
        public string Dt_Txt { get; set; } = string.Empty;
        public Main Main { get; set; } = new();
        public List<Weather> Weather { get; set; } = new();
    }
}