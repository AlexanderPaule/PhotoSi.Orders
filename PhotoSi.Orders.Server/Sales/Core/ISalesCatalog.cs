using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Sales.Core
{
	public interface ISalesCatalog
	{
		Task UpsertAsync(IEnumerable<Category> categories);
		Task UpsertAsync(IEnumerable<Product> categories);
	}
}