using Microsoft.EntityFrameworkCore;
using PhotoSi.Products.Core;
using PhotoSi.Products.Data;
using PhotoSi.Products.Data.Context;
using PhotoSi.Products.Data.Translation;

namespace PhotoSi.Products.Setup;

public static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiProducts(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<IProductsGateway, CoreEngine>();
		
		services.AddScoped<IProductsRepository, ProductsRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<ProductsDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
}