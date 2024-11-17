using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Core;

public interface IProductsGateway
{
	Task UpsertAsync(IEnumerable<Product> categories);
	Task<RequestResult<Product, Guid>> GetAllProductsAsync();
}