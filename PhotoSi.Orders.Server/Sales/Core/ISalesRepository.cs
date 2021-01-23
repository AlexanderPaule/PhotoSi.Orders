using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Sales.Core
{
	internal interface ISalesRepository
	{
		Task SaveAsync(Order order);
		Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id);
		Task<RequestResult<Order, Guid>> GetAllOrdersAsync();
		Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> ids);
		Task<RequestResult<Product, Guid>> GetAllProductsAsync();
		Task<bool> ExistsCategoryAsync(Guid id);
		Task<bool> ExistsOrderAsync(Guid id);
		Task UpsertAsync(IEnumerable<Category> categories);
		Task UpsertAsync(IEnumerable<Product> products);
	}
}