using System.Collections.Generic;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Demo.Controllers;

public interface IDemoDataCatalog
{
	IEnumerable<Category> GetCategories();
	IEnumerable<Product> GetProducts();
}