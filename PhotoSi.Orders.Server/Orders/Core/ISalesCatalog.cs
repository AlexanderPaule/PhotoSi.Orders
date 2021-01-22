using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Core
{
	public interface ISalesCatalog
	{
		Task UpsertAsync(IEnumerable<Category> categories);
		Task UpsertAsync(IEnumerable<Product> categories);
	}
}