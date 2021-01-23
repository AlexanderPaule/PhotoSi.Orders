using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Core
{
	internal interface ISalesPersistence
	{
		Task SaveAsync(Order order);
		Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id);
		Task<RequestResult<Order, Guid>> GetAllOrdersAsync();
		Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> ids);
		Task<bool> ExistsCategoryAsync(Guid id);
		Task<bool> ExistsOrderAsync(Guid id);
		Task Upsert(IEnumerable<Category> categories);
		Task Upsert(IEnumerable<Product> products);
	}
}