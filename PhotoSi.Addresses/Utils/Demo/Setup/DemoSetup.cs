using PhotoSi.Addresses.Utils.Demo.Controllers;
using PhotoSi.Addresses.Utils.Demo.Data;

namespace PhotoSi.Addresses.Utils.Demo.Setup;

internal static class DemoSetup
{
	public static IServiceCollection AddPhotoSiDemo(this IServiceCollection services)
	{
		services.AddScoped<IDemoDataCatalog, DemoDataCatalog>();

		return services;
	}
}
