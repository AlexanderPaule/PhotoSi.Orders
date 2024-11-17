using PhotoSi.API.Products.Models;
using PhotoSi.API.Utils;
using PhotoSi.Products.Core;

namespace PhotoSi.API.Products.Validation;

internal class Validator : IValidator
{
	private readonly IProductsGateway _productsGateway;

	public Validator(IProductsGateway productsGateway)
	{
		_productsGateway = productsGateway;
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

		if (!await _productsGateway.ExistsCategoryAsync(product.Category.Id))
			validationResult.AddErrorMessage<Guid>($"{nameof(ProductModel)}.{nameof(ProductModel.Category)}.{nameof(CategoryModel.Id)} specified category does not exists [{product.Category.Id}] ");

		return validationResult;
	}
}