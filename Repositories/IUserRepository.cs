using WeatherAPI.Models;

namespace WeatherAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserResponse>> GetAllUsersFromApiAsync();
        Task<UserResponse> GetUserByIdFromApiAsync(int id);
        Task<List<PostResponse>> GetAllPostsFromApiAsync();
        Task<PostResponse> GetPostByIdFromApiAsync(int id);
        Task<List<PostResponse>> GetPostsByUserIdFromApiAsync(int userId);
    }
}