using PhotoSi.Products.Controllers.Models;
using PhotoSi.Products.Core;
using PhotoSi.Sales.Utils;

namespace PhotoSi.Products.Controllers.Validation;

internal class Validator : IValidator
{
	private readonly ICheckGateway _checkGateway;

	public Validator(ICheckGateway checkGateway)
	{
		_checkGateway = checkGateway;
	}

	public async Task<ValidationResult> ValidateAsync(ProductModel product)
	{
		var validationResult = ValidationResult.New();

		if (product.Id == Guid.Empty)
			validationResult.AddErrorMessage<Guid>($"{nameof(ProductModel)}.{nameof(ProductModel.Id)} property is required");

		if (product.Category.Id == Guid.Empty)
			validationResult.AddErrorMessage<Guid>($"{nameof(ProductModel)}.{nameof(ProductModel.Category)}.{nameof(CategoryModel.Id)} property is required");

		if (!validationResult.IsValid)
			return validationResult;

		if (!await _checkGateway.ExistsCategoryAsync(product.Category.Id))
			validationResult.AddErrorMessage<Guid>($"{nameof(ProductModel)}.{nameof(ProductModel.Category)}.{nameof(CategoryModel.Id)} specified category does not exists [{product.Category.Id}] ");

		return validationResult;
	}
}