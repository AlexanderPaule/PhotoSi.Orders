using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Sales.Sales.Core;
using PhotoSi.Sales.Sales.Data;
using PhotoSi.Sales.Sales.Data.Context;
using PhotoSi.Sales.Sales.Data.Translation;

namespace PhotoSi.Sales.Sales.Setup;

internal static class SalesSetup
{
	public static IServiceCollection AddPhotoSiSales(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<SalesPortal>();
		services.AddScoped<IOrdersEngine>(x => x.GetService<SalesPortal>());
		services.AddScoped<ICheckGateway>(x => x.GetService<SalesPortal>());
		services.AddScoped<IProductsPortal>(x => x.GetService<SalesPortal>());
		services.AddScoped<IDemoPortal>(x => x.GetService<SalesPortal>());
		
		services.AddScoped<ISalesRepository, SalesRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<SalesDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
}