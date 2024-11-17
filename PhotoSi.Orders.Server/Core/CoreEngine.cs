using System;
using System.Threading.Tasks;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.Orders.Core;

public class CoreEngine : IOrdersGateway
{
	private readonly IOrdersRepository _repository;

	public CoreEngine(IOrdersRepository repository)
	{
		_repository = repository;
	}

	public Task ProcessAsync(Order order)
	{
		return _repository
			.SaveAsync(order);
	}

	public Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id)
	{
		return _repository
			.GetOrderAsync(id);
	}

	public Task<RequestResult<Order, Guid>> GetAllOrdersAsync()
	{
		return _repository
			.GetAllOrdersAsync();
	}

	public Task<bool> ExistsOrderAsync(Guid id)
	{
		return _repository
			.ExistsOrderAsync(id);
	}
}