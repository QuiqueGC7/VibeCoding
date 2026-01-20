namespace WeatherAPI.Models.External
{
    public class OpenWeatherMapResponse
    {
        public string Name { get; set; } = string.Empty;
        public Sys Sys { get; set; } = new();
        public Main Main { get; set; } = new();
        public List<Weather> Weather { get; set; } = new();
        public Wind Wind { get; set; } = new();
    }

    public class Sys
    {
        public string Country { get; set; } = string.Empty;
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public int Humidity { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; } = string.Empty;
    }

    public class Wind
    {
        public double Speed { get; set; }
    }
}
