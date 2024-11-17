using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.Orders.Core;

internal interface IOrdersRepository
{
	Task SaveAsync(Order order);
	Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id);
	Task<RequestResult<Order, Guid>> GetAllOrdersAsync();
	Task<bool> ExistsOrderAsync(Guid id);
}