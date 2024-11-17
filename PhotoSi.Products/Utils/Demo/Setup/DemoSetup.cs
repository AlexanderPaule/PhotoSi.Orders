using PhotoSi.Products.Utils.Demo.Controllers;
using PhotoSi.Products.Utils.Demo.Data;
using PhotoSi.Products.Utils.Demo.Translation;

namespace PhotoSi.Products.Utils.Demo.Setup;

internal static class DemoSetup
{
	public static IServiceCollection AddPhotoSiDemo(this IServiceCollection services)
	{
		services.AddScoped<IDemoDataCatalog, DemoDataCatalog>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}
}
