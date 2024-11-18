using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Core;

public interface IProductsGateway
{
	Task UpsertAsync(IEnumerable<Product> categories);
	Task UpsertAsync(IEnumerable<Category> categories);
	Task<RequestResult<Product, Guid>> GetAllProductsAsync();
	Task<bool> ExistsCategoryAsync(Guid id);
	Task<IDictionary<Guid, bool>> ExistsProductsAsync(IEnumerable<Guid> productsIds);
}