using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponse> LoginAsync(LoginDto loginDto);

		Task<AuthResponse> RegisterAsync(RegisterDto registerDto);
	}
}

