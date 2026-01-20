using WeatherAPI.Models;
using WeatherAPI.Repositories;

namespace WeatherAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheRepository _cache;

        public UserService(IUserRepository userRepository, ICacheRepository cache)
        {
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var cacheKey = "all_users";
            
            var cached = _cache.Get<List<UserResponse>>(cacheKey);
            if (cached != null)
            {
                cached.ForEach(u => u.FromCache = true);
                return cached;
            }

            var users = await _userRepository.GetAllUsersFromApiAsync();
            users.ForEach(u => u.FromCache = false);
            
            _cache.Set(cacheKey, users);
            
            return users;
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var cacheKey = $"user_{id}";
            
            var cached = _cache.Get<UserResponse>(cacheKey);
            if (cached != null)
            {
                cached.FromCache = true;
                return cached;
            }

            var user = await _userRepository.GetUserByIdFromApiAsync(id);
            user.FromCache = false;
            
            _cache.Set(cacheKey, user);
            
            return user;
        }

        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            var cacheKey = "all_posts";
            
            var cached = _cache.Get<List<PostResponse>>(cacheKey);
            if (cached != null)
            {
                cached.ForEach(p => p.FromCache = true);
                return cached;
            }

            var posts = await _userRepository.GetAllPostsFromApiAsync();
            posts.ForEach(p => p.FromCache = false);
            
            _cache.Set(cacheKey, posts);
            
            return posts;
        }

        public async Task<PostResponse> GetPostByIdAsync(int id)
        {
            var cacheKey = $"post_{id}";
            
            var cached = _cache.Get<PostResponse>(cacheKey);
            if (cached != null)
            {
                cached.FromCache = true;
                return cached;
            }

            var post = await _userRepository.GetPostByIdFromApiAsync(id);
            post.FromCache = false;
            
            _cache.Set(cacheKey, post);
            
            return post;
        }

        public async Task<PostsByUserResponse> GetPostsByUserIdAsync(int userId)
        {
            var cacheKey = $"posts_by_user_{userId}";
            
            var cached = _cache.Get<PostsByUserResponse>(cacheKey);
            if (cached != null)
            {
                cached.FromCache = true;
                return cached;
            }

            // Obtener usuario y sus posts en paralelo para optimizar rendimiento
            var userTask = GetUserByIdAsync(userId);
            var postsTask = _userRepository.GetPostsByUserIdFromApiAsync(userId);

            await Task.WhenAll(userTask, postsTask);

            var user = await userTask;
            var posts = await postsTask;

            var result = new PostsByUserResponse
            {
                UserId = userId,
                UserName = user.Name,
                Posts = posts,
                TotalPosts = posts.Count,
                FromCache = false
            };

            _cache.Set(cacheKey, result);
            
            return result;
        }
    }
}