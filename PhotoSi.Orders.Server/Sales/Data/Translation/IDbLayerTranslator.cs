using System;
using PhotoSi.Orders.Server.Sales.Core.Models;
using PhotoSi.Orders.Server.Sales.Data.Models;

namespace PhotoSi.Orders.Server.Sales.Data.Translation
{
	internal interface IDbLayerTranslator
	{
		OrderEntity Translate(Order source);
		Order Translate(OrderEntity source);
		
		Product Translate(ProductEntity source);
		ProductEntity Translate(Product source);
		OrderedProductEntity TranslateOrdered(Product product, Guid orderId);
		OrderedOptionEntity TranslateOrdered(Option source, Guid productId);

		CategoryEntity Translate(Category source);
		
		OptionEntity Translate(Option source, Guid referencedProductId);
	}
}