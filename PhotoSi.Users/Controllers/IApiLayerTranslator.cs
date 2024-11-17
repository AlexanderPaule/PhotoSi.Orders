using PhotoSi.Users.Controllers.Models;
using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Controllers;

public interface IApiLayerTranslator
{
	UserModel Translate(User source);
	User Translate(UserModel source);
}