namespace WeatherAPI.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public AddressInfo Address { get; set; } = new();
        public CompanyInfo Company { get; set; } = new();
        public bool FromCache { get; set; }
    }

    public class AddressInfo
    {
        public string Street { get; set; } = string.Empty;
        public string Suite { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public GeoLocation Geo { get; set; } = new();
    }

    public class GeoLocation
    {
        public string Lat { get; set; } = string.Empty;
        public string Lng { get; set; } = string.Empty;
    }

    public class CompanyInfo
    {
        public string Name { get; set; } = string.Empty;
        public string CatchPhrase { get; set; } = string.Empty;
        public string Bs { get; set; } = string.Empty;
    }
}