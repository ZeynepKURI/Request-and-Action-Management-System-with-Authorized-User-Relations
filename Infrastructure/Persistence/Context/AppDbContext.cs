using Core.Entities;
using Core.Entities.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Actions> Actions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User ve Actions arasındaki ilişkiler (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedActions)   // Bir User birden fazla Action'a sahip olabilir
                .WithOne(a => a.AssignedUser)     // Bir Action bir User'a atanır
                .HasForeignKey(a => a.AssignedTo) // Aksiyonun kullanıcıya atanmasını sağlar
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silindiğinde aksiyon silinmesin

            // Request ve Actions arasındaki ilişkiler (One-to-Many)
            modelBuilder.Entity<Request>()
                .HasMany(r => r.Actions)  // Bir Request birden fazla Action'a sahip olabilir
                .WithOne(a => a.Request)  // Bir Action bir Request'e ait olur
                .HasForeignKey(a => a.RequestId) // Aksiyonun Request'e ait olduğunu belirtir
                .OnDelete(DeleteBehavior.Cascade); // Talep silindiğinde aksiyonlar da silinsin

            // User ve Request arasındaki ilişkiler (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedRequests) // Kullanıcı birden fazla Request'e sahip olabilir
                .WithOne(r => r.User)             // Request bir User'a ait
                .HasForeignKey(r => r.UserId)     // UserId foreign key
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde talepler de silinsin
        }
    }
}
