using System.Collections.Generic;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Demo.Controllers.Data
{
	public interface IDemoDataCatalog
	{
		IEnumerable<Category> GetCategories();
		IEnumerable<Product> GetProducts();
	}
}