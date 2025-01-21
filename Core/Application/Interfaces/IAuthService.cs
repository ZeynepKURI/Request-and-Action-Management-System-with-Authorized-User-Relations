using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface IAuthService
	{
		Task<LoginResponse> UserLoginAsync(LoginDto loginDto);

		Task<RegisterResponse> UserRegisterAsync(RegisterDto registerDto);
	}
}

