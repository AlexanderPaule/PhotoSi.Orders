using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Core;

public interface IDemoGateway
{
	Task UpsertAsync(IEnumerable<Address> users);
}