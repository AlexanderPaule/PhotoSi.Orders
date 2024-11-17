using System;
using System.Linq;
using PhotoSi.Orders.Core.Models;
using PhotoSi.Orders.Data.Models;

namespace PhotoSi.Orders.Data.Translation;

internal class DbLayerTranslator : IDbLayerTranslator
{
	public OrderEntity Translate(Order source)
	{
		return new OrderEntity
		{
			Id = source.Id,
			Products = source.ProductsIds.Select(x => TranslateOrdered(x, source.Id)),
			CreatedOn = source.CreatedOn
		};
	}

	public Order Translate(OrderEntity source)
	{
		return new Order
		{
			Id = source.Id,
			ProductsIds = source.Products.Select(x => x.ProductId),
			CreatedOn = source.CreatedOn
		};
	}

	public OrderedProductEntity TranslateOrdered(Guid productId, Guid orderId)
	{
		return new OrderedProductEntity
		{
			Id = Guid.NewGuid(),
			ProductId = productId,
			OrderId = orderId
		};
	}
}