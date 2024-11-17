using PhotoSi.Users.Controllers.Models;
using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Controllers.Translation;

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