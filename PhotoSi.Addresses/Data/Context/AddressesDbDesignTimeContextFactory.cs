using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PhotoSi.Addresses.Data.Context;

internal class AddressesDbDesignTimeContextFactory : IDesignTimeDbContextFactory<AddressesDbContext>
{
	public AddressesDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var builder = new DbContextOptionsBuilder<AddressesDbContext>();
		builder.UseSqlServer(configuration.GetConnectionString("Addresses"));
		return new AddressesDbContext(builder.Options);
	}
}