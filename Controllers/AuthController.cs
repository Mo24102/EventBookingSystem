using EventBookingSystem.DTOs;
using EventBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {           
            
            try
            {
                var user = await _authService.RegisterAsync(dto);
                return Ok(new { message = "User registered successfully", data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Registration failed", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var user = await _authService.LoginAsync(dto);
                if (user == null)
                    return Unauthorized(new { message = "Invalid email or password" });

                return Ok(new { message = "Login successful", data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Login failed", error = ex.Message });
            }
        }

    }
}
