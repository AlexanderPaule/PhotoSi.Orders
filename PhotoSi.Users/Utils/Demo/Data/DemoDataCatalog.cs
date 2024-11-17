using PhotoSi.Users.Core.Models;
using PhotoSi.Users.Utils.Demo.Controllers;

namespace PhotoSi.Users.Utils.Demo.Data;

internal class DemoDataCatalog : IDemoDataCatalog
{
	public IEnumerable<User> GetUsers()
	{
		return
		[
			new User
			{
				Id = new Guid("211174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Name = "Billy",
				Surname = "Harris"
			},
			new User
			{
				Id = new Guid("311174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				Name = "Jack",
				Surname = "Jankins"
			}
		];
	}
}