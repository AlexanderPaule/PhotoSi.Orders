using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Users.Data.Context;

internal class DbContextFactory : IDbContextFactory
{
	private readonly string _connectionString;

	public DbContextFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public UsersDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();

		optionsBuilder
			.UseSqlServer(_connectionString, opts =>
			{
				opts.EnableRetryOnFailure();
			});

		return new UsersDbContext(optionsBuilder.Options);
	}
}
