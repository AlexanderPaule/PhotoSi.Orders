using Microsoft.EntityFrameworkCore;
using PhotoSi.Products.Core;
using PhotoSi.Products.Core.Models;
using PhotoSi.Products.Data.Context;
using PhotoSi.Products.Data.Models;
using PhotoSi.Products.Data.Translation;

namespace PhotoSi.Products.Data;

internal class ProductsRepository : IProductsRepository
{
	private readonly IDbContextFactory _dbContextFactory;
	private readonly IDbLayerTranslator _dbLayerTranslator;

	public ProductsRepository(IDbContextFactory dbContextFactory, IDbLayerTranslator dbLayerTranslator)
	{
		_dbContextFactory = dbContextFactory;
		_dbLayerTranslator = dbLayerTranslator;
	}

	public async Task<RequestResult<Product, Guid>> GetAllProductsAsync()
	{
		var productEntities = await GetProductEntities(Enumerable.Empty<Guid>());

		return RequestResult<Product, Guid>.New(
			requestedObjects: productEntities.Select(x => _dbLayerTranslator.Translate(x)),
			searchedIds: productEntities.Select(x => x.Id));
	}

	public async Task<bool> ExistsCategoryAsync(Guid id)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Categories
			.AnyAsync(x => x.Id == id);
	}

	public async Task UpsertAsync(IEnumerable<Category> categories)
	{
		var categoryEntities = categories
			.Select(_dbLayerTranslator.Translate)
			.ToList();

		await using var dbContext = _dbContextFactory
			.CreateDbContext();

		await dbContext.Categories.UpsertBulkAsync(
			entities: categoryEntities,
			dbFilter: e => categoryEntities.Select(x => x.Id).Contains(e.Id));

		await dbContext.SaveChangesAsync();
	}

	public async Task UpsertAsync(IEnumerable<Product> products)
	{
		var productEntities = products
			.Select(_dbLayerTranslator.Translate)
			.ToList();

		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		await salesDbContext.Products.UpsertBulkAsync(
			entities: productEntities,
			dbFilter: e => productEntities.Select(x => x.Id).Contains(e.Id));

		await salesDbContext.SaveChangesAsync();
	}

	private async Task<List<ProductEntity>> GetProductEntities(IEnumerable<Guid> ids)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Products
			.Include(x => x.Category)
			.Where(x => !ids.Any() || ids.Contains(x.Id))
			.ToListAsync();
	}

	public async Task<IDictionary<Guid, bool>> ExistsProductsAsync(List<Guid> productsIds)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		var products = await salesDbContext
			.Products
			.Include(x => x.Category)
			.Where(x => !productsIds.Any() || productsIds.Contains(x.Id))
			.Select(x => x.Id)
			.ToListAsync();

		return productsIds
			.ToDictionary(x => x, x => products.Any(p => p == x));
	}
}