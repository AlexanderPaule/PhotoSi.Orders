using Microsoft.EntityFrameworkCore;
using PhotoSi.Products.Data.Models;

namespace PhotoSi.Products.Data.Context;

public class ProductsDbContext : DbContext
{
	public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Names and Schema
		modelBuilder.Entity<ProductEntity>(entity => entity.ToTable("Catalog", "products"));
		modelBuilder.Entity<CategoryEntity>(entity => entity.ToTable("Categories", "products"));

		// Products
		modelBuilder
			.Entity<ProductEntity>()
			.HasOne(p => p.Category)
			.WithMany(b => b.Products)
			.HasForeignKey(p => p.CategoryId);
	}

	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<CategoryEntity> Categories { get; set; }

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
