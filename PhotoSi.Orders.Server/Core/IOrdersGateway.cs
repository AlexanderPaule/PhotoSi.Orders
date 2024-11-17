using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.Orders.Core;

public interface IOrdersGateway
{
	Task ProcessAsync(Order order);
	Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id);
	Task<RequestResult<Order, Guid>> GetAllOrdersAsync();
	Task<bool> ExistsOrderAsync(Guid id);
}