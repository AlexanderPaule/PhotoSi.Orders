using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PhotoSi.Products.Data.Context;

internal class ProductsDbDesignTimeContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
	public ProductsDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var builder = new DbContextOptionsBuilder<ProductsDbContext>();
		builder.UseSqlServer(configuration.GetConnectionString("Products"));
		return new ProductsDbContext(builder.Options);
	}
}