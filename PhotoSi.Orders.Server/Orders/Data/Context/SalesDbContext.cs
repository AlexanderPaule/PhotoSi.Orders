using System;
using System.Linq;
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
		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<OptionEntity> Options { get; set; }

		public override int SaveChanges()
		{
			ChangeTracker
				.Entries()
				.Where(e => e.Entity is TimeTrackedEntity)
				.ToList()
				.ForEach(e =>
				{
					var baseEntity = (TimeTrackedEntity)e.Entity;
					
					switch (e.State)
					{
						case EntityState.Added:
							baseEntity.DbCreated = DateTimeOffset.Now;
							baseEntity.DbUpdated = DateTimeOffset.Now;
							break;
						case EntityState.Modified:
							baseEntity.DbUpdated = DateTimeOffset.Now;
							break;
					}
				});
			
			return base.SaveChanges();
		}
	}
}
