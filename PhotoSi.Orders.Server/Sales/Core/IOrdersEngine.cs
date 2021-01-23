using System;
using System.Threading.Tasks;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Sales.Core
{
	public interface IOrdersEngine
	{
		Task ProcessAsync(Order order);
		Task<RequestResult<Order, Guid>> GetAsync(Guid id);
		Task<RequestResult<Order, Guid>> GetAllAsync();
	}
}