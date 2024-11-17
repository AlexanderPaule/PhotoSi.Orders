using Microsoft.EntityFrameworkCore;
using PhotoSi.Addresses.Data.Models;

namespace PhotoSi.Addresses.Data.Context;

internal class AddressesDbContext : DbContext
{
	public AddressesDbContext(DbContextOptions<AddressesDbContext> options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Names and Schema
		modelBuilder.Entity<AddressEntity>(entity => entity.ToTable("Catalog", "addresses"));
	}

	public DbSet<AddressEntity> Addresses { get; set; }

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
