using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Register (Kayıt Ol)
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(new { message = result });
        }

        // Login (Giriş Yap)
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { token });
        }

        // Admin işlemleri (Sadece Admin erişebilir)
        [HttpGet("admin-action")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminAction()
        {
            return Ok("Bu işlem sadece Admin tarafından yapılabilir.");
        }

        // Kullanıcı işlemleri (Sadece User ve Admin erişebilir)
        [HttpGet("user-action")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UserAction()
        {
            return Ok("Bu işlem hem User hem Admin tarafından yapılabilir.");
        }
    }
}
