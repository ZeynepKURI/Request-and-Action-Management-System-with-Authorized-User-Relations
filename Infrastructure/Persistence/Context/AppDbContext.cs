using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
		{ }


		public DbSet<User> users { get; set; }

		public DbSet<Request> requests { get; set; }

		public DbSet<Actions> actions { get; set; }

	}
}

