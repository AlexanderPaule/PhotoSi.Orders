using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Validation
{
	public interface IValidator
	{
		ValidationResult Validate(OrderModel order);
	}
}