using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Data
{
	internal class SalesPersistence : ISalesPersistence
	{
		public Task SaveAsync(Order order)
		{
			throw new NotImplementedException();
		}

		public Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> ids)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ExistsCategoryAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}