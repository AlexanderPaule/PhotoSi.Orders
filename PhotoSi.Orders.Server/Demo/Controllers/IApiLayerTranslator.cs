using PhotoSi.Sales.Demo.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Demo.Controllers
{
	public interface IApiLayerTranslator
	{
		ProductModel Translate(Product source);
		CategoryModel Translate(Category source);
	}
}