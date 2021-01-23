using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Sales.Core;
using PhotoSi.Orders.Server.Sales.Data;
using PhotoSi.Orders.Server.Sales.Data.Context;
using PhotoSi.Orders.Server.Sales.Data.Translation;

namespace PhotoSi.Orders.Server.Sales.Setup
{
	internal static class SalesSetup
	{
		public static IServiceCollection AddPhotoSiSales(this IServiceCollection services, string dbConnectionString)
		{
			services.AddScoped<SalesCatalog>();
			services.AddScoped<IOrdersEngine>(x => x.GetService<SalesCatalog>());
			services.AddScoped<ICheckGateway>(x => x.GetService<SalesCatalog>());
			services.AddScoped<ISalesPortal>(x => x.GetService<SalesCatalog>());
			
			services.AddScoped<ISalesPersistence, SalesPersistence>();
			services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
			services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
			services.AddDbContext<SalesDbContext>(o => o.UseSqlServer(dbConnectionString));

			return services;
		}
	}
}