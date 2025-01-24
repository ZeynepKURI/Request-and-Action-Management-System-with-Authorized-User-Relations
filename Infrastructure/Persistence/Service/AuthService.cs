using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;  // JWT ayarlarını almak için yapılandırma

        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }


        // Kullanıcı kayıt işlemi
        public async Task<AuthResponse> RegisterAsync(RegisterDto registerDto)
        {

            // Aynı e-posta adresiyle kullanıcı olup olmadığını kontrol et
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


        // Kullanıcı giriş işlemi
        public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
        {
            // Kullanıcıyı e-posta ve şifre ile kontrol et
            var user = _appDbContext.Users
                .FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            // Kullanıcı bulunamazsa hata mesajı döndür
            if (user == null)
                return new AuthResponse(false, "Invalid email or password");

            // Token oluştur
            var token = GenerateJwtToken(user);
            return new AuthResponse(true, "Login successful", token);
        }


        // Kullanıcı bulunduysa JWT token oluştur
        private string GenerateJwtToken(User user)
        {
            // Kullanıcı bilgilerine dayalı claimler oluştur
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name), // Kullanıcı adı
                new Claim(ClaimTypes.Email, user.Email), // Kullanıcı e-postası
                new Claim(ClaimTypes.Role, user.Role) // Kullanıcı rolü
            };

            // JWT oluşturmak için kullanılan anahtar

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"], // Token sağlayıcısı
                audience: _configuration["Jwt:Audience"], // Token alıcıları
                claims: claims, // Kullanıcı bilgileri
                expires: DateTime.Now.AddHours(1), // Token süresi (1 saat)
                signingCredentials: creds); // İmzalama bilgileri

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
