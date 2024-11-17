using PhotoSi.API.Addresses.Models;
using PhotoSi.API.Utils;

namespace PhotoSi.API.Addresses.Validation;

internal class Validator : IValidator
{
	public ValidationResult Validate(AddrressModel product)
	{
		var validationResult = ValidationResult.New();

		if (product.Id == Guid.Empty)
			validationResult.AddErrorMessage<Guid>($"{nameof(AddrressModel)}.{nameof(AddrressModel.Id)} property is required");

		if (!validationResult.IsValid)
			return validationResult;

		return validationResult;
	}
}