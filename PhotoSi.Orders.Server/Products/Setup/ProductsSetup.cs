using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Sales.Products.Controllers;
using PhotoSi.Sales.Products.Translation;
using PhotoSi.Sales.Products.Validation;

namespace PhotoSi.Sales.Products.Setup;

internal static class ProductsSetup
{
	public static IServiceCollection AddPhotoSiProducts(this IServiceCollection services)
	{
		services.AddScoped<IValidator, Validator>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}
}
