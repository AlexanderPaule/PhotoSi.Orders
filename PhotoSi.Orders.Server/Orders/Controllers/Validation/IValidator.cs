using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Validation
{
	public interface IValidator
	{
		Task<ValidationResult> ValidateAsync(OrderModel order);
	}
}