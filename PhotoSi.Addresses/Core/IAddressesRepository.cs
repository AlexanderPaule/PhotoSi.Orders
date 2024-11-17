using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Core;

internal interface IAddressesRepository
{
	Task UpsertAsync(IEnumerable<Address> users);
	Task<RequestResult<Address, Guid>> GetAllAddressesAsync();
}