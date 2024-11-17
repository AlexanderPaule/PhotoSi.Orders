using PhotoSi.API.Products.Models;
using PhotoSi.Products.Core.Models;

namespace PhotoSi.API.Products;

public interface IApiLayerTranslator
{
	ProductModel Translate(Product source);
	Product Translate(ProductModel source);
}