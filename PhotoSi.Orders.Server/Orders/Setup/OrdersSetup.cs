using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;

namespace PhotoSi.Orders.Server.Orders.Setup
{
	public static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services)
		{
			services.AddScoped<IValidator, Validator>();
			
			return services;
		}
	}
}
