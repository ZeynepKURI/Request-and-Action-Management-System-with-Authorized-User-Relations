using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorks;
using Persistence.UnitOfWorks;
using Application.Interfaces.Service;
using Persistence.Services;
using Application.Mapping;
using Infrastructure.Data;
using Persistence.Service;
using Application.Interfaces.Repository;
using Core.Entities;
using Persistence.Repository;

namespace Persistence.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext'i ekle ve Npgsql (PostgreSQL) bağlantısını yapılandır
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default"), // Connection string yapılandırması
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName) // Migrations doğru assembly'den uygulanacak
                ), ServiceLifetime.Scoped);

            // JWT ile kimlik doğrulama işlemi yapılandır
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],  // İssuer'ı al
                    ValidAudience = configuration["Jwt:Audience"],  // Audience'ı al
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)) // Signing key'i al
                };
            });

            // Authorization işlemleri için roller bazında politikalar ekle
            services.AddAuthorization(options =>
            {
                // 'User' rolü için politika
                options.AddPolicy("User", policy => policy.RequireRole("User"));

                // 'Admin' rolü için politika
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });

            // HttpContextAccessor'ı ekle (HttpContext'e erişmek için)
            services.AddHttpContextAccessor();

            // Servisleri Dependency Injection ile ekle
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<IRepository<Request>, Repository<Request>>();      //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IRepository<Actions>, Repository<Actions>>();
            services.AddScoped<IRequestService, RequestService>();  // RequestService'i IRequestService olarak enjekte et
            services.AddScoped<IAuthService, AuthService>();  // AuthService'i IAuthService olarak enjekte et
            services.AddScoped<IUnitOfWork, UnitOfWork>();  // UnitOfWork servislerini enjekte et
           // ActionService'i IActionService olarak enjekte et

            return services;
        }
    }
}
