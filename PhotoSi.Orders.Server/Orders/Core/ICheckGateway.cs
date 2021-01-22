using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Core
{
	internal interface ICheckGateway
	{
		Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> productsIds);
		Task<bool> ExistsCategoryAsync(Guid id);
		Task<bool> ExistsOrderAsync(Guid id);
	}
}