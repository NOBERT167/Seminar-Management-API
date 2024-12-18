using Microsoft.AspNetCore.Mvc;
using SeminarAPI.Interfaces;
using System.Threading.Tasks;

namespace SeminarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(request.Username, request.Password, request.Email, request.Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginUserAsync(request.Username, request.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            try
            {
                var result = await _authService.UpdateUserAsync(request.Username, request.Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterAdminAsync(request.Username, request.Password, request.Email, request.Name);
                return Ok(new { message = "Administrator registered successfully.", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login/admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAdminAsync(request.Username, request.Password);
                return Ok(new { message = "Admin login successful.", data = result });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }

    // DTOs for API requests
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserRequest
    {
        public string Username { get; set; }
        public string Name { get; set; }
    }
}
