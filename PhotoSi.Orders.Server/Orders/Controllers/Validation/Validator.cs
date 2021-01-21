using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Validation
{
	internal class Validator : IValidator
	{
		public async Task<ValidationResult> ValidateAsync(OrderModel order)
		{
			var validationResult = ValidationResult.New();

			if (order.Id == Guid.Empty)
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} property is required");
			
			return validationResult;
		}
	}
}