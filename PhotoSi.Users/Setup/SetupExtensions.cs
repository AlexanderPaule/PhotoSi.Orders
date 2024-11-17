using Microsoft.EntityFrameworkCore;
using PhotoSi.Users.Core;
using PhotoSi.Users.Data;
using PhotoSi.Users.Data.Context;
using PhotoSi.Users.Data.Translation;

namespace PhotoSi.Users.Setup;

public static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiUsers(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<IUsersGateway, CoreEngine>();

		services.AddScoped<IUsersRepository, UsersRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<UsersDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
}