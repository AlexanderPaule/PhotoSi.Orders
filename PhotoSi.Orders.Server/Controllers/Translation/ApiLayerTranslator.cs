using PhotoSi.Orders.Controllers.Models;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.Orders.Controllers.Translation;

public class ApiLayerTranslator : IApiLayerTranslator
{
	public Order Translate(OrderModel source)
	{
		return new Order
		{
			Id = source.Id,
			AddressId = source.AddressId,
			UserId = source.UserId,
			ProductsIds = source.ProductsIds,
			CreatedOn = source.CreatedOn
		};
	}

	public OrderModel Translate(Order source)
	{
		return new OrderModel
		{
			Id = source.Id,
			AddressId = source.AddressId,
			UserId = source.UserId,
			ProductsIds = source.ProductsIds,
			CreatedOn = source.CreatedOn
		};
	}
}