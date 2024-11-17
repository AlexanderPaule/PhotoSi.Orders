using PhotoSi.API.Users;
using PhotoSi.API.Users.Models;
using PhotoSi.Users.Core.Models;

namespace PhotoSi.API.Users.Translation;

internal class ApiLayerTranslator : IApiLayerTranslator
{

	public UserModel Translate(User source)
	{
		return new UserModel
		{
			Id = source.Id,
			Name = source.Name,
			Surname = source.Surname
		};
	}

	public User Translate(UserModel source)
	{
		return new User
		{
			Id = source.Id,
			Name = source.Name,
			Surname = source.Surname
		};
	}
}