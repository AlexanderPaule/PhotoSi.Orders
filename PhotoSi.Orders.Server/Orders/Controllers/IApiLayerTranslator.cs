using PhotoSi.Orders.Server.Orders.Models;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers
{
	public interface IApiLayerTranslator
	{
		Order Translate(OrderModel source);
		OrderModel Translate(Order source);
	}
}