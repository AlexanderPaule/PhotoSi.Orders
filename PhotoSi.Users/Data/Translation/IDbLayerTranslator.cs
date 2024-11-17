using PhotoSi.Users.Core.Models;
using PhotoSi.Users.Data.Models;

namespace PhotoSi.Users.Data.Translation;

internal interface IDbLayerTranslator
{
	User Translate(UserEntity source);
	UserEntity Translate(User source);
}