using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PhotoSi.Sales.Sales.Data.Context;

internal class SalesDbDesignTimeContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
{
	public SalesDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var builder = new DbContextOptionsBuilder<SalesDbContext>();
		builder.UseSqlServer(configuration.GetConnectionString("Sales"));
		return new SalesDbContext(builder.Options);
	}
}