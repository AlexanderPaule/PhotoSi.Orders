using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Core;
using PhotoSi.Orders.Data;
using PhotoSi.Orders.Data.Context;
using PhotoSi.Orders.Data.Translation;

namespace PhotoSi.Orders.Setup;

public static class SetupExtensions
{
	public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services, string dbConnectionString)
	{
		services.AddScoped<IOrdersGateway, CoreEngine>();

		services.AddScoped<IOrdersRepository, OrdersRepository>();
		services.AddScoped<IDbContextFactory>(x => new DbContextFactory(dbConnectionString));
		services.AddScoped<IDbLayerTranslator, DbLayerTranslator>();
		services.AddDbContext<OrdersDbContext>(o => o.UseSqlServer(dbConnectionString));

		return services;
	}
}