using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Core;

public interface IDemoGateway
{
	Task UpsertAsync(IEnumerable<Category> categories);
	Task UpsertAsync(IEnumerable<Product> categories);
}