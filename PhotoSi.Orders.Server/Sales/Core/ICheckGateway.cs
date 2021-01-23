using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Sales.Core
{
	internal interface ICheckGateway
	{
		Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> productsIds);
		Task<bool> ExistsCategoryAsync(Guid id);
		Task<bool> ExistsOrderAsync(Guid id);
	}
}