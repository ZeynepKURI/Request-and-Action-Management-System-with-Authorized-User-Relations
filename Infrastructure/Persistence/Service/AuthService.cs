using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Service;
using Core.Entities;
using Core.Entities.Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Services
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

        public async Task<AuthResponse> RegisterAsync(RegisterDto registerDto)
        {
            // Kullanıcı adı kontrolü
            if (_appDbContext.Users.Any(u => u.Email == registerDto.Email))
                return new AuthResponse(false, "Email already exists");

            // Yeni kullanıcı ekleme
            var user = new User
            {
                Name = registerDto.Username,
                Password = registerDto.Password, // Şifre hashlenmeli
                Email = registerDto.Email,
                Role = registerDto.Role // Rol atanıyor
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync(); // Veritabanına kaydet
            return new AuthResponse(true, "User registered successfully");
        }

        public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
        {
            // Kullanıcıyı e-posta ve şifre ile kontrol et
            var user = _appDbContext.Users
                .FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
                return new AuthResponse(false, "Invalid email or password");

            // Token oluştur
            var token = GenerateJwtToken(user);
            return new AuthResponse(true, "Login successful", token);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role) // Kullanıcı rolü
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
