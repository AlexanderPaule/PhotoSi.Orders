using System.Threading.Tasks;
using PhotoSi.Orders.Controllers.Models;
using PhotoSi.Orders.Utils;

namespace PhotoSi.Orders.Controllers;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(OrderModel order);
}