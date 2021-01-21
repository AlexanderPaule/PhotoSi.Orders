using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Data.Translation
{
	internal interface IDbLayerTranslator
	{
		OrderEntity Translate(Order order);
	}
}