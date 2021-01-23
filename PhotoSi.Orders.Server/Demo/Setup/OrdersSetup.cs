using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Demo.Controllers.Data;
using PhotoSi.Orders.Server.Demo.Controllers.Translation;

namespace PhotoSi.Orders.Server.Demo.Setup
{
	internal static class DemoSetup
	{
		public static IServiceCollection AddPhotoSiDemo(this IServiceCollection services)
		{
			services.AddScoped<IDemoDataCatalog, DemoDataCatalog>();
			services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

			return services;
		}
	}
}
