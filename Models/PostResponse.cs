namespace WeatherAPI.Models
{
    public class PostResponse
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool FromCache { get; set; }
    }

    public class PostsByUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<PostResponse> Posts { get; set; } = new();
        public int TotalPosts { get; set; }
        public bool FromCache { get; set; }
    }
}