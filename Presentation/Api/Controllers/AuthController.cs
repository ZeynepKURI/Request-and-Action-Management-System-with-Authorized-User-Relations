using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers

{
    // AuthController: Kimlik doğrulama ve yetkilendirme işlemleri için API endpointlerini içerir.

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;   // Kimlik doğrulama işlemleri için kullanılan servis.


        // Dependency Injection (Bağımlılık enjeksiyonu) ile kimlik doğrulama servisi alınır.
        public AuthController(IAuthService authService)
        {
            _authService = authService;  // Service instance'ı sınıfa atanır.
        }

        // Kullanıcı kayıt işlemini gerçekleştiren endpoint.
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(new { message = result });  // Kayıt başarılı olursa bir mesaj döndürülür.
        }

        // Kullanıcı giriş işlemini gerçekleştiren endpoint.
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

        // Hem User hem de Admin rollerinin erişebileceği bir işlem.

        [HttpGet("user-action")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UserAction()
        {
            return Ok("Bu işlem hem User hem Admin tarafından yapılabilir.");
        }
    }
}
