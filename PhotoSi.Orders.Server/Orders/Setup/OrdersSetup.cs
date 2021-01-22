using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Orders.Controllers.Translation;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Data;
using PhotoSi.Orders.Server.Orders.Data.Context;
using PhotoSi.Orders.Server.Orders.Data.Translation;

namespace PhotoSi.Orders.Server.Orders.Setup
{
	internal static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services, string dbConnectionString)
		{
			services.AddScoped<IValidator, Validator>();
			services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();
			
			services.AddScoped<SalesPortal>();
			services.AddScoped<IOrdersEngine>(x => x.GetService<SalesPortal>());
			services.AddScoped<ICheckGateway>(x => x.GetService<SalesPortal>());
			
			services.AddScoped<ISalesPersistence, SalesPersistence>();
			services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
			services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
			services.AddDbContext<SalesDbContext>(o => o.UseSqlServer(dbConnectionString));

			return services;
		}
	}
}
