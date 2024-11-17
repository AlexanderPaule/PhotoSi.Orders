using PhotoSi.API.Users.Models;
using PhotoSi.Users.Core.Models;

namespace PhotoSi.API.Users;

public interface IApiLayerTranslator
{
	UserModel Translate(User source);
	User Translate(UserModel source);
}