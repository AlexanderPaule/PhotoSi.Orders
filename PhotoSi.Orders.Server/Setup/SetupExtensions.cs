using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Controllers;
using PhotoSi.Orders.Controllers.Translation;
using PhotoSi.Orders.Controllers.Validation;
using PhotoSi.Orders.Core;
using PhotoSi.Orders.Data;
using PhotoSi.Orders.Data.Context;
using PhotoSi.Orders.Data.Translation;

namespace PhotoSi.Orders.Setup;

internal static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<CoreEngine>();
		services.AddScoped<IOrdersEngine>(x => x.GetService<CoreEngine>());
		services.AddScoped<ICheckGateway>(x => x.GetService<CoreEngine>());

		services.AddScoped<IOrdersRepository, SalesRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<OrdersDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
	public static IServiceCollection AddPhotoSiOrdersAPI(this IServiceCollection services)
	{
		services.AddScoped<IValidator, Validator>();
		services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();

		return services;
	}

}