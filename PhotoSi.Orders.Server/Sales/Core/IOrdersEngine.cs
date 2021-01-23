using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Sales.Core
{
	public interface IOrdersEngine
	{
		Task ProcessAsync(Order order);
		Task<RequestResult<Order, Guid>> GetAsync(Guid id);
		Task<RequestResult<Order, Guid>> GetAllAsync();
	}
}