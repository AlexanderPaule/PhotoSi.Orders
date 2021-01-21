using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Orders.Controllers.Translation;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Data;

namespace PhotoSi.Orders.Server.Orders.Setup
{
	internal static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services)
		{
			services.AddScoped<IValidator, Validator>();
			services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();
			
			services.AddScoped<SalesPortal>();
			services.AddScoped<IOrdersEngine>(x => x.GetService<SalesPortal>());
			services.AddScoped<IOrdersEngine>(x => x.GetService<SalesPortal>());
			
			services.AddScoped<ISalesPersistence, SalesPersistence>();

			return services;
		}
	}
}
