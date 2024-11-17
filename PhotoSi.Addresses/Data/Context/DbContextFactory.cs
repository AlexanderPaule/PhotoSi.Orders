using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Addresses.Data.Context;

internal class DbContextFactory : IDbContextFactory
{
	private readonly string _connectionString;

	public DbContextFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public AddressesDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<AddressesDbContext>();

		optionsBuilder
			.UseSqlServer(_connectionString, opts =>
			{
				opts.EnableRetryOnFailure();
			});

		return new AddressesDbContext(optionsBuilder.Options);
	}
}
