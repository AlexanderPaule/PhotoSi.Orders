using System;
using PhotoSi.Orders.Core.Models;
using PhotoSi.Orders.Data.Models;

namespace PhotoSi.Orders.Data.Translation;

internal interface IDbLayerTranslator
{
	OrderEntity Translate(Order source);
	Order Translate(OrderEntity source);
	OrderedProductEntity TranslateOrdered(Guid productId, Guid orderId);
}