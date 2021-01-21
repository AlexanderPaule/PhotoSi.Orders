using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Data
{
	internal interface ISalesPersistence
	{
		Task SaveAsync(Order order);
		Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id);
	}
}