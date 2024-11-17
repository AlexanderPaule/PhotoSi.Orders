using PhotoSi.API.Addresses.Models;
using PhotoSi.API.Utils;

namespace PhotoSi.API.Addresses;

public interface IValidator
{
	ValidationResult Validate(AddrressModel product);
}