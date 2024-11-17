using PhotoSi.Products.Core.Models;
using PhotoSi.Products.Utils.Demo.Models;

namespace PhotoSi.Products.Utils.Demo.Controllers;

public interface IApiLayerTranslator
{
	DemoProductModel Translate(Product source);
	DemoCategoryModel Translate(Category source);
}