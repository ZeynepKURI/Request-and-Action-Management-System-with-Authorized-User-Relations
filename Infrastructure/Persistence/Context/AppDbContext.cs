using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        // AppDbContext sınıfı, veritabanı işlemleri için kullanılan Entity Framework Core DbContext sınıfını türetir
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

        // DbSet'ler: Veritabanı tablolarına karşılık gelen özellikler
        // Bu özellikler, uygulamanın erişebileceği koleksiyonları temsil eder.
        public DbSet<Request> Requests { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API ile ilişkileri tanımlayabilirsiniz (isteğe bağlı)
            modelBuilder.Entity<Request>()
                .HasMany(r => r.Actions) // Request birden fazla Action'a sahip olabilir
                .WithOne(a => a.Request) // Her Action bir Request'e ait
                .HasForeignKey(a => a.RequestId) // Action'da RequestId yabancı anahtar
                .OnDelete(DeleteBehavior.Cascade); // Request silindiğinde ilişkili Actions silinir
        }
    }
}