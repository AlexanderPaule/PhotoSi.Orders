using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Core;

internal class CoreEngine : IAddressesGateway, IDemoGateway
{
	private readonly IAddressesRepository _repository;

	public CoreEngine(IAddressesRepository repository)
	{
		_repository = repository;
	}

	public Task UpsertAsync(IEnumerable<Address> addresses)
	{
		return _repository
			.UpsertAsync(addresses);
	}

	public Task<RequestResult<Address, Guid>> GetAllAddressesAsync()
	{
		return _repository
			.GetAllAddressesAsync();
	}
}