using System;
using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Data.Translation
{
	internal interface IDbLayerTranslator
	{
		OrderEntity Translate(Order source);
		Order Translate(OrderEntity source);
		Product Translate(ProductEntity source);
	}
}