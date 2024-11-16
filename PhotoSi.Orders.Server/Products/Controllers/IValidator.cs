using System.Threading.Tasks;
using PhotoSi.Sales.Products.Models;
using PhotoSi.Sales.Utils;

namespace PhotoSi.Sales.Products.Controllers;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(ProductModel product);
}