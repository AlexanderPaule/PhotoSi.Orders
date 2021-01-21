using Microsoft.EntityFrameworkCore;
using PhotoSi.Orders.Server.Orders.Data.Models;

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
		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<OptionEntity> Options { get; set; }
		public DbSet<OrderedProductEntity> OrderedProducts { get; set; }
	}
}
