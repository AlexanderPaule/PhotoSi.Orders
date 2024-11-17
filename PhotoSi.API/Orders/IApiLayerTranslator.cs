using PhotoSi.API.Orders.Models;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.API.Orders;

public interface IApiLayerTranslator
{
	Order Translate(OrderModel source);
	OrderModel Translate(Order source);
}