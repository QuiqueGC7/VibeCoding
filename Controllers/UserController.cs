using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        [HttpGet("usuarios")]
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAllUsersAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { error = "Error al obtener usuarios", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Obtener un usuario por ID
        /// </summary>
        [HttpGet("usuarios/{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "El ID debe ser mayor que 0" });
            }

            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("404"))
                {
                    return NotFound(new { error = $"Usuario con ID {id} no encontrado" });
                }
                return StatusCode(500, new { error = "Error al obtener usuario", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Obtener todos los posts
        /// </summary>
        [HttpGet("posts")]
        public async Task<ActionResult<List<PostResponse>>> GetAllPosts()
        {
            try
            {
                var result = await _userService.GetAllPostsAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { error = "Error al obtener posts", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Obtener un post por ID
        /// </summary>
        [HttpGet("posts/{id}")]
        public async Task<ActionResult<PostResponse>> GetPostById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "El ID debe ser mayor que 0" });
            }

            try
            {
                var result = await _userService.GetPostByIdAsync(id);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("404"))
                {
                    return NotFound(new { error = $"Post con ID {id} no encontrado" });
                }
                return StatusCode(500, new { error = "Error al obtener post", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Obtener todos los posts de un usuario específico
        /// </summary>
        [HttpGet("usuarios/{userId}/posts")]
        public async Task<ActionResult<PostsByUserResponse>> GetPostsByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { error = "El ID del usuario debe ser mayor que 0" });
            }

            try
            {
                var result = await _userService.GetPostsByUserIdAsync(userId);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("404"))
                {
                    return NotFound(new { error = $"Usuario con ID {userId} no encontrado" });
                }
                return StatusCode(500, new { error = "Error al obtener posts del usuario", detalle = ex.Message });
            }
        }
    }
}