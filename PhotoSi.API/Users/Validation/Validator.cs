using PhotoSi.API.Users.Models;
using PhotoSi.API.Utils;

namespace PhotoSi.API.Users.Validation;

internal class Validator : IValidator
{
	public async Task<ValidationResult> ValidateAsync(UserModel product)
	{
		var validationResult = ValidationResult.New();

		if (product.Id == Guid.Empty)
			validationResult.AddErrorMessage<Guid>($"{nameof(UserModel)}.{nameof(UserModel.Id)} property is required");

		if (!validationResult.IsValid)
			return validationResult;

		return validationResult;
	}
}