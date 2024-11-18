using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Core;

public interface IProductsRepository
{
	Task<bool> ExistsCategoryAsync(Guid id);
	Task UpsertAsync(IEnumerable<Category> categories);
	Task UpsertAsync(IEnumerable<Product> products);
	Task<RequestResult<Product, Guid>> GetAllProductsAsync();
	Task<IDictionary<Guid, bool>> ExistsProductsAsync(IEnumerable<Guid> productsIds);
}