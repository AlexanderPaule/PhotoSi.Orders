using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PhotoSi.Orders.Server.Orders.Controllers.Translation;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Setup
{
	internal static class OrdersSetup
	{
		public static IServiceCollection AddPhotoSiOrders(this IServiceCollection services)
		{
			services.AddScoped<IValidator, Validator>();
			services.AddScoped<IApiLayerTranslator, ApiLayerTranslator>();
			
			services.AddScoped<OrdersEngine>();
			services.AddScoped<IOrdersEngine>(x => x.GetService<OrdersEngine>());
			services.AddScoped<IOrdersEngine>(x => x.GetService<OrdersEngine>());

			return services;
		}
	}

	internal class OrdersEngine : IOrdersEngine, ICheckGateway
	{
		public Task ProcessAsync(Order order)
		{
			throw new NotImplementedException();
		}

		public Task<RequestResult<Order>> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<Guid> productsIds)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ExistsCategoryAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
