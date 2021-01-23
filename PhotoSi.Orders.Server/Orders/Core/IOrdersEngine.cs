using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Core
{
	public interface IOrdersEngine
	{
		Task ProcessAsync(Order order);
		Task<RequestResult<Order, Guid>> GetAsync(Guid id);
		Task<RequestResult<Order, Guid>> GetAllAsync();
	}
}