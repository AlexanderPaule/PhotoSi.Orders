using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Utils.Demo.Controllers;

public interface IDemoDataCatalog
{
	IEnumerable<Category> GetCategories();
	IEnumerable<Product> GetProducts();
}