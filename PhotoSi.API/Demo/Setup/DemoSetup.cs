using PhotoSi.API.Demo.Controllers;
using PhotoSi.API.Demo.Data;

namespace PhotoSi.API.Demo.Setup;

internal static class DemoSetup
{
	public static IServiceCollection AddPhotoSiDemo(this IServiceCollection services)
	{
		services.AddScoped<IDemoDataCatalog, DemoDataCatalog>();

		return services;
	}
}
