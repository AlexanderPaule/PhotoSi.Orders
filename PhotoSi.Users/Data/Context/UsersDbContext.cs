using Microsoft.EntityFrameworkCore;
using PhotoSi.Users.Data.Models;

namespace PhotoSi.Users.Data.Context;

internal class UsersDbContext : DbContext
{
	public UsersDbContext(DbContextOptions<UsersDbContext> options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Names and Schema
		modelBuilder.Entity<UserEntity>(entity => entity.ToTable("Users", "accounts"));
	}

	public DbSet<UserEntity> Users { get; set; }

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
