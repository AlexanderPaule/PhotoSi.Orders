using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Orders.Controllers;
using PhotoSi.Orders.Server.Orders.Translation;
using PhotoSi.Orders.Server.Orders.Validation;

namespace PhotoSi.Orders.Server.Orders.Setup
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
