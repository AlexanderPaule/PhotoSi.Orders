using Microsoft.EntityFrameworkCore;
using PhotoSi.Addresses.Core;
using PhotoSi.Addresses.Data;
using PhotoSi.Addresses.Data.Context;
using PhotoSi.Addresses.Data.Translation;

namespace PhotoSi.Addresses.Setup;

public static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiAddresses(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<IAddressesGateway, CoreEngine>();

		services.AddScoped<IAddressesRepository, AddressesRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<AddressesDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
}