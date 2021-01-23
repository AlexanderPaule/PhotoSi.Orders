using System.Collections.Generic;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Demo.Controllers.Data
{
	public interface IDemoDataCatalog
	{
		IEnumerable<Category> GetCategories();
		IEnumerable<Product> GetProducts();
	}
}