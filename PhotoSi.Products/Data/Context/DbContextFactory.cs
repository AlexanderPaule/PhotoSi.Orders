using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Products.Data.Context;

internal class DbContextFactory : IDbContextFactory
{
	private readonly string _connectionString;

	public DbContextFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public ProductsDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();

		optionsBuilder
			.UseSqlServer(_connectionString, opts =>
			{
				opts.EnableRetryOnFailure();
			});

		return new ProductsDbContext(optionsBuilder.Options);
	}
}
