using PhotoSi.Users.Controllers.Models;
using PhotoSi.Users.Utils;

namespace PhotoSi.Users.Controllers;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(UserModel product);
}