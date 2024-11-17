using PhotoSi.Orders.Controllers.Models;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.Orders.Controllers;

public interface IApiLayerTranslator
{
	Order Translate(OrderModel source);
	OrderModel Translate(Order source);
}