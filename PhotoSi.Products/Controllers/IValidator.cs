using PhotoSi.Products.Controllers.Models;
using PhotoSi.Sales.Utils;

namespace PhotoSi.Products.Controllers;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(ProductModel product);
}