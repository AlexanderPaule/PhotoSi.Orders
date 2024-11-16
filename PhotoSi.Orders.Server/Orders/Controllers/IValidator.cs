using System.Threading.Tasks;
using PhotoSi.Sales.Orders.Models;
using PhotoSi.Sales.Utils;

namespace PhotoSi.Sales.Orders.Controllers;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(OrderModel order);
}