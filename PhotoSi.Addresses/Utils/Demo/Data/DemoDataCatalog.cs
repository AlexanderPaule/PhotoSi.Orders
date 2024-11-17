using PhotoSi.Addresses.Core.Models;
using PhotoSi.Addresses.Utils.Demo.Controllers;

namespace PhotoSi.Addresses.Utils.Demo.Data;

internal class DemoDataCatalog : IDemoDataCatalog
{
	public IEnumerable<Address> GetAddresses()
	{
		return
		[
			new Address
			{
				Id = new Guid("222274E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				UserId = new Guid("211174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				City = "New York",
				Country = "US"
			},
			new Address
			{
				Id = new Guid("333374E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				UserId = new Guid("311174E4-37B7-44EE-A8A2-EE920C6FAB9D"),
				City = "Casablanca",
				Country = "Marocco"
			}
		];
	}
}