using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Core;

public interface IAddressesRepository
{
	Task UpsertAsync(IEnumerable<Address> users);
	Task<RequestResult<Address, Guid>> GetAllAddressesAsync();
	Task<bool> ExistsAddressAsync(Guid addressId);
}