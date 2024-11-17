using PhotoSi.Products.Utils.Demo.Controllers;
using PhotoSi.Products.Utils.Demo.Data;

namespace PhotoSi.Products.Utils.Demo.Setup;

internal static class DemoSetup
{
	public static IServiceCollection AddPhotoSiDemo(this IServiceCollection services)
	{
		services.AddScoped<IDemoDataCatalog, DemoDataCatalog>();

		return services;
	}
}
