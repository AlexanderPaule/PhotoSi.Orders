using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PhotoSi.Users.Data.Context;

internal class UsersDbDesignTimeContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
	public UsersDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var builder = new DbContextOptionsBuilder<UsersDbContext>();
		builder.UseSqlServer(configuration.GetConnectionString("Users"));
		return new UsersDbContext(builder.Options);
	}
}