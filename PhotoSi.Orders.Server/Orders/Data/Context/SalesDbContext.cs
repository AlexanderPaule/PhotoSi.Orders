using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
			// Orders
			modelBuilder
				.Entity<OrderEntity>()
				.HasOne(p => p.Category)
				.WithMany(b => b.Orders)
				.HasForeignKey(p => p.CategoryId);
			
			// Products
			modelBuilder
				.Entity<ProductEntity>()
				.HasOne(p => p.Category)
				.WithMany(b => b.Products)
				.HasForeignKey(p => p.CategoryId);
			
			modelBuilder
				.Entity<OrderedProductEntity>()
				.HasOne(p => p.ReferencedProduct)
				.WithMany(b => b.OrderedProducts)
				.HasForeignKey(p => p.ProductId);
			
			// Options
			modelBuilder
				.Entity<OptionEntity>()
				.HasOne(p => p.Product)
				.WithMany(b => b.Options)
				.HasForeignKey(p => p.ProductId);
			
			modelBuilder
				.Entity<OrderedOptionEntity>()
				.HasOne(p => p.OrderedProduct)
				.WithMany(b => b.CustomOptions)
				.HasForeignKey(p => p.OrderedProductId)
				.OnDelete(DeleteBehavior.NoAction);
			
			modelBuilder
				.Entity<OrderedOptionEntity>()
				.HasOne(p => p.ReferencedOption)
				.WithMany(b => b.CustomizedOptions)
				.HasForeignKey(p => p.OptionId);
		}

		public DbSet<OrderEntity> Orders { get; set; }
		public DbSet<OrderedProductEntity> OrderedProducts { get; set; }
		public DbSet<OrderedOptionEntity> OrderedOptions { get; set; }
		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<OptionEntity> Options { get; set; }

		public override int SaveChanges()
		{
			return SaveChangesAsync().Result;
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
			
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
