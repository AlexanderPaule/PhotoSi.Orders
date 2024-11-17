using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PhotoSi.Orders.Data.Context;

internal class SalesDbDesignTimeContextFactory : IDesignTimeDbContextFactory<OrdersDbContext>
{
	public OrdersDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var builder = new DbContextOptionsBuilder<OrdersDbContext>();
		builder.UseSqlServer(configuration.GetConnectionString("Orders"));
		return new OrdersDbContext(builder.Options);
	}
}