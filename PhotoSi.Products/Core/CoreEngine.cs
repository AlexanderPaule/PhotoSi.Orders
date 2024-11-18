using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Core;

public class CoreEngine : IProductsGateway
{
	private readonly IProductsRepository _repository;

	public CoreEngine(IProductsRepository repository)
	{
		_repository = repository;
	}

	public Task UpsertAsync(IEnumerable<Category> categories)
	{
		return _repository
			.UpsertAsync(categories);
	}

	public Task UpsertAsync(IEnumerable<Product> categories)
	{
		return _repository
			.UpsertAsync(categories);
	}

	public Task<RequestResult<Product, Guid>> GetAllProductsAsync()
	{
		return _repository
			.GetAllProductsAsync();
	}

	public Task<bool> ExistsCategoryAsync(Guid id)
	{
		return _repository
			.ExistsCategoryAsync(id);
	}

	public Task<IDictionary<Guid, bool>> ExistsProductsAsync(IEnumerable<Guid> productsIds)
	{
		return _repository
			.ExistsProductsAsync(productsIds);
	}
}