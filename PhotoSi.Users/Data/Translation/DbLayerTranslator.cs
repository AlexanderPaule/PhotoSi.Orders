using PhotoSi.Users.Core.Models;
using PhotoSi.Users.Data.Models;

namespace PhotoSi.Users.Data.Translation;

internal class DbLayerTranslator : IDbLayerTranslator
{
	public User Translate(UserEntity source)
	{
		return new User
		{
			Id = source.Id,
			Name = source.Name,
			Surname = source.Surname
		};
	}

	public UserEntity Translate(User source)
	{
		return new UserEntity
		{
			Id = source.Id,
			Name = source.Name,
			Surname = source.Surname
		};
	}
}