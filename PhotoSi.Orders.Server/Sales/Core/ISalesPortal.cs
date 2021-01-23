using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Sales.Core
{
	public interface ISalesPortal
	{
		Task UpsertAsync(IEnumerable<Category> categories);
		Task UpsertAsync(IEnumerable<Product> categories);
	}
}