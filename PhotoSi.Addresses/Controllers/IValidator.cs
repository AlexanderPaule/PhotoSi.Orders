using PhotoSi.Addresses.Controllers.Models;
using PhotoSi.Addresses.Utils;

namespace PhotoSi.Addresses.Controllers;

public interface IValidator
{
	ValidationResult Validate(AddrressModel product);
}