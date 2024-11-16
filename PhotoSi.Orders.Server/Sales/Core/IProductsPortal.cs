using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Sales.Core;

public interface IProductsPortal
{
	Task UpsertAsync(IEnumerable<Product> categories);
	Task<RequestResult<Product, Guid>> GetAllProductsAsync();
}