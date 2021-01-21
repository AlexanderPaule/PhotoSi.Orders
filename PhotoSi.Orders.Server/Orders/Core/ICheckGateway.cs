using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Core
{
	internal interface ICheckGateway
	{
		Task<IEnumerable<Product>> GetProducts(IEnumerable<Guid> productsIds);
		Task<bool> ExistsCategory(Guid id);
	}
}