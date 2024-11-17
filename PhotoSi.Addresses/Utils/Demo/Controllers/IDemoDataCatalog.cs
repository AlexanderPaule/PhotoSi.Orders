using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Utils.Demo.Controllers;

public interface IDemoDataCatalog
{
	IEnumerable<Address> GetAddresses();
}