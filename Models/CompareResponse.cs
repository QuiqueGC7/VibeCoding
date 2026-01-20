namespace WeatherAPI.Models
{
    public class CompareResponse
    {
        public List<CityComparison> Comparacion { get; set; } = new();
        public CityComparison? MasCalida { get; set; }
        public CityComparison? MasFria { get; set; }
        public int DiferenciaTemperatura { get; set; }
    }

    public class CityComparison
    {
        public string Ciudad { get; set; } = string.Empty;
        public int Temperatura { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}