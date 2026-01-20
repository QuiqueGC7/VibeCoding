using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(int id);
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse> GetPostByIdAsync(int id);
        Task<PostsByUserResponse> GetPostsByUserIdAsync(int userId);
    }
}