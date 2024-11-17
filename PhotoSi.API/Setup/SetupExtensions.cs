namespace PhotoSi.API.Setup;

internal static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiOrdersAPI(this IServiceCollection services)
	{
		services.AddScoped<Addresses.IValidator, Addresses.Validation.Validator>();
		services.AddScoped<Addresses.IApiLayerTranslator, Addresses.Translation.ApiLayerTranslator>();

		return services;
	}

	public static IServiceCollection AddPhotoSiAddressesAPI(this IServiceCollection services)
	{
		services.AddScoped<Orders.IValidator, Orders.Validation.Validator>();
		services.AddScoped<Orders.IApiLayerTranslator, Orders.Translation.ApiLayerTranslator>();

		return services;
	}

	public static IServiceCollection AddPhotoSiProductsAPI(this IServiceCollection services)
	{
		services.AddScoped<Products.IValidator, Products.Validation.Validator>();
		services.AddScoped<Products.IApiLayerTranslator, Products.Translation.ApiLayerTranslator>();

		return services;
	}

	public static IServiceCollection AddPhotoSiUsersAPI(this IServiceCollection services)
	{
		services.AddScoped<Users.IValidator, Users.Validation.Validator>();
		services.AddScoped<Users.IApiLayerTranslator, Users.Translation.ApiLayerTranslator>();

		return services;
	}
}