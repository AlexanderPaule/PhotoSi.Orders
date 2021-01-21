using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Core
{
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