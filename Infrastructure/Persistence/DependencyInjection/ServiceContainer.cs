﻿using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Application.Interfaces;
using Persistence.Service;
using Application.Interfaces.UnitOfWorks;
using Persistence.UnitOfWorks;

namespace Persistence.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext with Npgsql (PostgreSQL) connection
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default"), // Connection string is taken from the configuration
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName) // Ensure the migrations are in the correct assembly
                ), ServiceLifetime.Scoped);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });



            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}