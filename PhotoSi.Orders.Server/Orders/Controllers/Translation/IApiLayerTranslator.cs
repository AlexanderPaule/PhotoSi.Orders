using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Test.Orders
{
	public interface IApiLayerTranslator
	{
		Order Translate(OrderModel source);
	}
}