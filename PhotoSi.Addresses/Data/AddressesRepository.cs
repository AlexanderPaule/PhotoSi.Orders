using Microsoft.EntityFrameworkCore;
using PhotoSi.Addresses.Core;
using PhotoSi.Addresses.Core.Models;
using PhotoSi.Addresses.Data.Context;
using PhotoSi.Addresses.Data.Models;
using PhotoSi.Addresses.Data.Translation;

namespace PhotoSi.Addresses.Data;

public class AddressesRepository : IAddressesRepository
{
	private readonly IDbContextFactory _dbContextFactory;
	private readonly IDbLayerTranslator _dbLayerTranslator;

	public AddressesRepository(IDbContextFactory dbContextFactory, IDbLayerTranslator dbLayerTranslator)
	{
		_dbContextFactory = dbContextFactory;
		_dbLayerTranslator = dbLayerTranslator;
	}

	public async Task<bool> ExistsAddressAsync(Guid addressId)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Addresses
			.AnyAsync(x => x.Id == addressId);
	}

	public async Task<RequestResult<Address, Guid>> GetAllAddressesAsync()
	{
		var entities = await GetAddressEntities(Enumerable.Empty<Guid>());

		return RequestResult<Address, Guid>.New(
			requestedObjects: entities.Select(x => _dbLayerTranslator.Translate(x)),
			searchedIds: entities.Select(x => x.Id));
	}

	public async Task UpsertAsync(IEnumerable<Address> addresses)
	{
		var entities = addresses
			.Select(_dbLayerTranslator.Translate)
			.ToList();

		await using var dbContext = _dbContextFactory
			.CreateDbContext();

		await dbContext.Addresses.UpsertBulkAsync(
			entities: entities,
			dbFilter: e => entities.Select(x => x.Id).Contains(e.Id));

		await dbContext.SaveChangesAsync();
	}

	private async Task<List<AddressEntity>> GetAddressEntities(IEnumerable<Guid> ids)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Addresses
			.Where(x => !ids.Any() || ids.Contains(x.Id))
			.ToListAsync();
	}
}