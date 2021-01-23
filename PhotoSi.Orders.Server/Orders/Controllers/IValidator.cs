using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Models;
using PhotoSi.Orders.Server.Orders.Validation;

namespace PhotoSi.Orders.Server.Orders.Controllers
{
	public interface IValidator
	{
		Task<ValidationResult> ValidateAsync(OrderModel order);
	}
}