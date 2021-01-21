using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Data
{
	internal interface ISalesPersistence
	{
		Task SaveAsync(Order order);
		Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id);
		Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> ids);
		Task<bool> ExistsCategoryAsync(Guid id);
	}
}