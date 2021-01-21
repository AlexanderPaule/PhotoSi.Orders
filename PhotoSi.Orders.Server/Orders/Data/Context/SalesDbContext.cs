using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Orders.Server.Orders.Data.Context
{
	internal class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        { }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public DbSet<OrderEntity> Orders { get; set; }
	}
}
