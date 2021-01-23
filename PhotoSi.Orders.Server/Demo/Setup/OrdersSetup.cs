using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Sales.Demo.Controllers;
using PhotoSi.Sales.Demo.Data;
using PhotoSi.Sales.Demo.Translation;

namespace PhotoSi.Sales.Demo.Setup
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
