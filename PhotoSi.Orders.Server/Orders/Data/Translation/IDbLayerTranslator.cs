using System;
using System.Collections.Generic;
using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Data.Translation
{
	internal interface IDbLayerTranslator
	{
		OrderEntity Translate(Order source, IEnumerable<ProductEntity> existingProducts);
		Order Translate(OrderEntity source);
		Product Translate(ProductEntity source);
		ProductEntity Translate(Product source);
		CategoryEntity Translate(Category source);
		OptionEntity Translate(Option source, Guid referencedProductId);
	}
}