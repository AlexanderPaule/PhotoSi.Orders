using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Orders.Data.Context;

public class DbContextFactory : IDbContextFactory
{
	private readonly string _connectionString;

	public DbContextFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public OrdersDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>();

		optionsBuilder
			.UseSqlServer(_connectionString, opts =>
			{
				opts.EnableRetryOnFailure();
			});

		return new OrdersDbContext(optionsBuilder.Options);
	}
}
