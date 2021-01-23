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
			services.AddScoped<SalesPortal>();
			services.AddScoped<IOrdersEngine>(x => x.GetService<SalesPortal>());
			services.AddScoped<ICheckGateway>(x => x.GetService<SalesPortal>());
			services.AddScoped<ISalesPortal>(x => x.GetService<SalesPortal>());
			
			services.AddScoped<ISalesRepository, SalesRepository>();
			services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
			services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
			services.AddDbContext<SalesDbContext>(o => o.UseSqlServer(dbConnectionString));

			return services;
		}
	}
}