﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class RegisterDto
	{
		[Required]
		public string UserName { get; set; }

		[Required , EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required, Compare(nameof(Password))]    //Compare atributu, iki farklı özelliğin (property) eşit olup olmadığını doğrular.
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}

