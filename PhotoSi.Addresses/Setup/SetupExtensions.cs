using Microsoft.EntityFrameworkCore;
using PhotoSi.Addresses.Controllers;
using PhotoSi.Addresses.Controllers.Translation;
using PhotoSi.Addresses.Controllers.Validation;
using PhotoSi.Addresses.Core;
using PhotoSi.Addresses.Data;
using PhotoSi.Addresses.Data.Context;
using PhotoSi.Addresses.Data.Translation;

namespace PhotoSi.Addresses.Setup;

internal static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiAddressesCore(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<CoreEngine>();
		services.AddScoped<IAddressesGateway>(x => x.GetService<CoreEngine>()!);
		services.AddScoped<IDemoGateway>(x => x.GetService<CoreEngine>()!);

		services.AddScoped<IAddressesRepository, AddressesRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<AddressesDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
	public static IServiceCollection AddPhotoSiAddressesAPI(this IServiceCollection services)
	{
		services.AddScoped<IValidator, Validator>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}
}