using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // appsettings.json dosyasını bulma yolu
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Çalışma dizini
                .AddJsonFile("appsettings.json")
                .Build();

            // Npgsql (PostgreSQL) bağlantısı
           
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}