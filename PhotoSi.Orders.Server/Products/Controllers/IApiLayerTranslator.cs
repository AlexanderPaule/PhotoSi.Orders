using PhotoSi.Sales.Products.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Products.Controllers;

public interface IApiLayerTranslator
{
	ProductModel Translate(Product source);
	Product Translate(ProductModel source);
}