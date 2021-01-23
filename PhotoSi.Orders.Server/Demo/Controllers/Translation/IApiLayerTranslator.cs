using PhotoSi.Orders.Server.Demo.Controllers.Models;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Demo.Controllers.Translation
{
	public interface IApiLayerTranslator
	{
		ProductModel Translate(Product source);
		CategoryModel Translate(Category source);
	}
}