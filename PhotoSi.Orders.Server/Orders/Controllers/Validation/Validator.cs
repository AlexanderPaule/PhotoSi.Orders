using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Validation
{
	internal class Validator : IValidator
	{
		public async Task<ValidationResult> ValidateAsync(OrderModel order)
		{
			return ValidationResult.New();
		}
	}
}