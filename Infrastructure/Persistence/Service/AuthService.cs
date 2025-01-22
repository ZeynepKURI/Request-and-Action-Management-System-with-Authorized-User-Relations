
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Service
{
	
        public class AuthService : IAuthService
        {
            private readonly AppDbContext _appDbContext;
            private readonly IConfiguration _configuration;

            public AuthService(AppDbContext appDbContext, IConfiguration configuration)
            {
                _appDbContext = appDbContext;
                _configuration = configuration;
            }

            // user için register metodu
            public async Task<RegisterResponse> UserRegisterAsync(RegisterDto registerDto)
            {
                var existingUser = await FindUserByEmail(registerDto.Email);
                if (existingUser != null)
                {
                    return new RegisterResponse(false, "User already exists");
                }

                var newUser = new User
                {
                    Name = registerDto.UserName,
                    Email = registerDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
                };

                _appDbContext.users.Add(newUser);
                await _appDbContext.SaveChangesAsync();

                return new RegisterResponse(true, "Admin registered successfully");
            }

            // user için login metod
            public async Task<LoginResponse> UserLoginAsync(LoginDto loginDto)
            {
                var user = await FindUserByEmail(loginDto.Email);
                if (user== null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    return new LoginResponse(false, "Invalid credentials");
                }

                var token = GenerateJWTToken(user, "User");
                return new LoginResponse(true, token);
            }

       

      
            // Helper method for finding user by email
            private async Task<User> FindUserByEmail(string email)
            {
                return await _appDbContext.users.FirstOrDefaultAsync(u => u.Email == email);
            }

            // JWT Token generation (both for User)
            private string GenerateJWTToken(dynamic user, string role)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role) // Add role information here
            };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
}

