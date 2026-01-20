using System.Text.Json;
using WeatherAPI.Models;

namespace WeatherAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public UserRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration["JsonPlaceholder:BaseUrl"] ?? "https://jsonplaceholder.typicode.com";
        }

        public async Task<List<UserResponse>> GetAllUsersFromApiAsync()
        {
            var url = $"{_baseUrl}/users";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener usuarios: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserResponse>>(content, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

            if (users == null)
            {
                throw new InvalidOperationException("Error al deserializar usuarios");
            }

            return users;
        }

        public async Task<UserResponse> GetUserByIdFromApiAsync(int id)
        {
            var url = $"{_baseUrl}/users/{id}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener usuario: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

            if (user == null)
            {
                throw new InvalidOperationException("Error al deserializar usuario");
            }

            return user;
        }

        public async Task<List<PostResponse>> GetAllPostsFromApiAsync()
        {
            var url = $"{_baseUrl}/posts";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener posts: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<List<PostResponse>>(content, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

            if (posts == null)
            {
                throw new InvalidOperationException("Error al deserializar posts");
            }

            return posts;
        }

        public async Task<PostResponse> GetPostByIdFromApiAsync(int id)
        {
            var url = $"{_baseUrl}/posts/{id}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener post: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var post = JsonSerializer.Deserialize<PostResponse>(content, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

            if (post == null)
            {
                throw new InvalidOperationException("Error al deserializar post");
            }

            return post;
        }

        public async Task<List<PostResponse>> GetPostsByUserIdFromApiAsync(int userId)
        {
            var url = $"{_baseUrl}/posts?userId={userId}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al obtener posts del usuario: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<List<PostResponse>>(content, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

            if (posts == null)
            {
                throw new InvalidOperationException("Error al deserializar posts");
            }

            return posts;
        }
    }
}