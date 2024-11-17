using Microsoft.EntityFrameworkCore;
using PhotoSi.Products.Controllers;
using PhotoSi.Products.Controllers.Translation;
using PhotoSi.Products.Controllers.Validation;
using PhotoSi.Products.Core;
using PhotoSi.Products.Data;
using PhotoSi.Products.Data.Context;
using PhotoSi.Products.Data.Translation;

namespace PhotoSi.Products.Setup;

internal static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiProductsCore(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<CoreEngine>();
		services.AddScoped<ICheckGateway>(x => x.GetService<CoreEngine>()!);
		services.AddScoped<IProductsGateway>(x => x.GetService<CoreEngine>()!);
		services.AddScoped<IDemoGateway>(x => x.GetService<CoreEngine>()!);
		
		services.AddScoped<IProductsRepository, ProductsRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<ProductsDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
	public static IServiceCollection AddPhotoSiProductsAPI(this IServiceCollection services)
	{
		services.AddScoped<IValidator, Validator>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}
}