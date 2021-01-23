using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Controllers.Translation
{
	public interface IApiLayerTranslator
	{
		Order Translate(OrderModel source);
		OrderModel Translate(Order source);
		ProductModel Translate(Product source);
		CategoryModel Translate(Category source);
	}
}