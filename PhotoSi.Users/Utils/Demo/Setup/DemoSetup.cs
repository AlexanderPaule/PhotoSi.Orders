using PhotoSi.Users.Controllers;
using PhotoSi.Users.Controllers.Translation;
using PhotoSi.Users.Utils.Demo.Controllers;
using PhotoSi.Users.Utils.Demo.Data;

namespace PhotoSi.Users.Utils.Demo.Setup;

internal static class DemoSetup
{
	public static IServiceCollection AddPhotoSiDemo(this IServiceCollection services)
	{
		services.AddScoped<IDemoDataCatalog, DemoDataCatalog>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}
}
