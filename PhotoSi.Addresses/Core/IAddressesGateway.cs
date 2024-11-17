using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Core;

public interface IAddressesGateway
{
	Task UpsertAsync(IEnumerable<Address> categories);
	Task<RequestResult<Address, Guid>> GetAllAddressesAsync();
	Task<bool> ExistsAddressAsync(Guid addressId);
}