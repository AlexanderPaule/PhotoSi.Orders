using Microsoft.Extensions.DependencyInjection;

namespace PhotoSi.Orders.Server.Orders.Setup
{
	public static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services)
		{
			return services;
		}
	}
}
