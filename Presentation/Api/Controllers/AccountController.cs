
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IAuthService authService;

        public UserController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginUserIn(LoginDto loginDTO)
        {
            var result = await authService.UserLoginAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> RegisterUserDTO(RegisterDto registerDTO)

        {
            var result = await authService.UserRegisterAsync(registerDTO);

            return Ok(result);
        }

    }
}