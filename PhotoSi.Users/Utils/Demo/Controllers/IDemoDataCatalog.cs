using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Utils.Demo.Controllers;

public interface IDemoDataCatalog
{
	IEnumerable<User> GetUsers();
}