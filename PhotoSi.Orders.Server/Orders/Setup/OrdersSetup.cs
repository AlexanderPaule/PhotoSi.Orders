using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Sales.Orders.Controllers;
using PhotoSi.Sales.Orders.Translation;
using PhotoSi.Sales.Orders.Validation;

namespace PhotoSi.Sales.Orders.Setup
{
	internal static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services)
		{
			services.AddScoped<IValidator, Validator>();
			services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

			return services;
		}
	}
}
