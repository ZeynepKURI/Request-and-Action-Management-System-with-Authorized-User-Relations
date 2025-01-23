using System;
namespace Application.DTOs
{
	public record AuthResponse(bool Flag, string Mesaage, string Token = null);

}

