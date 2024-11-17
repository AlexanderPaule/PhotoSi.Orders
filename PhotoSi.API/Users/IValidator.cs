using PhotoSi.API.Users.Models;
using PhotoSi.API.Utils;

namespace PhotoSi.API.Users;

public interface IValidator
{
	Task<ValidationResult> ValidateAsync(UserModel product);
}