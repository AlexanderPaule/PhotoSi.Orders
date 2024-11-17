using PhotoSi.Products.Controllers.Models;
using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Controllers;

public interface IApiLayerTranslator
{
	ProductModel Translate(Product source);
	Product Translate(ProductModel source);
}