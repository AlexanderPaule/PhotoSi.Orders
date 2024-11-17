using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSi.Orders.Data.Models;

namespace PhotoSi.Orders.Data.Context;

internal class OrdersDbContext : DbContext
{
	public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Names and Schema
		modelBuilder.Entity<OrderEntity>(entity => entity.ToTable("Orders", "sales"));
		modelBuilder.Entity<OrderedProductEntity>(entity => entity.ToTable("OrderedProducts", "sales"));

		// Orders
		modelBuilder
			.Entity<OrderEntity>()
			.HasMany(p => p.Products);

		modelBuilder
			.Entity<OrderedProductEntity>()
			.HasOne(p => p.ReferencedOrder)
			.WithMany(b => b.Products)
			.HasForeignKey(p => p.OrderId);
	}

	public DbSet<OrderEntity> Orders { get; set; }
	public DbSet<OrderedProductEntity> OrderedProducts { get; set; }

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
