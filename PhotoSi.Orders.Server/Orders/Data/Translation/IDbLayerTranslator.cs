using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Data.Translation
{
	internal interface IDbLayerTranslator
	{
		OrderEntity Translate(Order order);
	}
}