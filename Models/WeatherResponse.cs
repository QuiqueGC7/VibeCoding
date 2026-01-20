namespace WeatherAPI.Models
{
    public class WeatherResponse
    {
        public string Ciudad { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public int Temperatura { get; set; }
        public int SensacionTermica { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Humedad { get; set; }
        public double Viento { get; set; }
        public DateTime Timestamp { get; set; }
        public bool FromCache { get; set; }
    }
}