﻿using Microsoft.EntityFrameworkCore;
using PhotoSi.Users.Core;
using PhotoSi.Users.Core.Models;
using PhotoSi.Users.Data.Context;
using PhotoSi.Users.Data.Models;
using PhotoSi.Users.Data.Translation;

namespace PhotoSi.Users.Data;

internal class UsersRepository : IUsersRepository
{
	private readonly IDbContextFactory _dbContextFactory;
	private readonly IDbLayerTranslator _dbLayerTranslator;

	public UsersRepository(IDbContextFactory dbContextFactory, IDbLayerTranslator dbLayerTranslator)
	{
		_dbContextFactory = dbContextFactory;
		_dbLayerTranslator = dbLayerTranslator;
	}

	public async Task<bool> ExistsUserAsync(Guid userId)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Users
			.AnyAsync(x => x.Id == userId);
	}

	public async Task<RequestResult<User, Guid>> GetAllUsersAsync()
	{
		var entities = await GetUsersEntities(Enumerable.Empty<Guid>());

		return RequestResult<User, Guid>.New(
			requestedObjects: entities.Select(x => _dbLayerTranslator.Translate(x)),
			searchedIds: entities.Select(x => x.Id));
	}

	public async Task UpsertAsync(IEnumerable<User> users)
	{
		var entities = users
			.Select(_dbLayerTranslator.Translate)
			.ToList();

		await using var dbContext = _dbContextFactory
			.CreateDbContext();

		await dbContext.Users.UpsertBulkAsync(
			entities: entities,
			dbFilter: e => entities.Select(x => x.Id).Contains(e.Id));

		await dbContext.SaveChangesAsync();
	}

	private async Task<List<UserEntity>> GetUsersEntities(IEnumerable<Guid> ids)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Users
			.Where(x => !ids.Any() || ids.Contains(x.Id))
			.ToListAsync();
	}
}