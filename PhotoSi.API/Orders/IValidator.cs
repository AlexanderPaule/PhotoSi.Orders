using PhotoSi.API.Orders.Models;
using PhotoSi.API.Utils;

namespace PhotoSi.API.Orders;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(OrderModel order);
}