using PhotoSi.Sales.Demo.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Demo.Controllers
{
	public interface IApiLayerTranslator
	{
		DemoProductModel Translate(Product source);
		DemoCategoryModel Translate(Category source);
	}
}