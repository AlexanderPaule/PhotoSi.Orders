using PhotoSi.Sales.Orders.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Orders.Controllers;

public interface IApiLayerTranslator
{
	Order Translate(OrderModel source);
	OrderModel Translate(Order source);
}