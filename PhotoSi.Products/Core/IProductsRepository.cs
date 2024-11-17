using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Core;

internal interface IProductsRepository
{
	Task<bool> ExistsCategoryAsync(Guid id);
	Task UpsertAsync(IEnumerable<Category> categories);
	Task UpsertAsync(IEnumerable<Product> products);
	Task<RequestResult<Product, Guid>> GetAllProductsAsync();
}