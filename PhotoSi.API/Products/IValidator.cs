using PhotoSi.API.Products.Models;
using PhotoSi.API.Utils;

namespace PhotoSi.API.Products;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(ProductModel product);
}