using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Sales.Sales.Data.Context;

internal class DbContextFactory : IDbContextFactory
{
	private readonly string _connectionString;

	public DbContextFactory(string connectionString)
	{
		_connectionString = connectionString;
	}
	
	public SalesDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();

		optionsBuilder
			.UseSqlServer(_connectionString, opts =>
			{
				opts.EnableRetryOnFailure();
			});

		return new SalesDbContext(optionsBuilder.Options);
	}
}
