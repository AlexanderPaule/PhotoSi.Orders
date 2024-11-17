using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSi.Orders.Core;
using PhotoSi.Orders.Core.Models;
using PhotoSi.Orders.Data.Context;
using PhotoSi.Orders.Data.Translation;

namespace PhotoSi.Orders.Data;

public class OrdersRepository : IOrdersRepository
{
	private readonly IDbContextFactory _dbContextFactory;
	private readonly IDbLayerTranslator _dbLayerTranslator;

	public OrdersRepository(IDbContextFactory dbContextFactory, IDbLayerTranslator dbLayerTranslator)
	{
		_dbContextFactory = dbContextFactory;
		_dbLayerTranslator = dbLayerTranslator;
	}

	public async Task SaveAsync(Order order)
	{
		var entityOrder = _dbLayerTranslator
			.Translate(order);

		var orderedProducts = order
			.ProductsIds
			.Select(x => _dbLayerTranslator.TranslateOrdered(x, order.Id));

		await using var orderDbContext = _dbContextFactory
			.CreateDbContext();

		await orderDbContext.AddAsync(entityOrder);
		await orderDbContext.SaveChangesAsync();
	}

	public async Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		var order = await salesDbContext
			.Orders
			.Include(x => x.Products)
			.Where(x => x.Id == id)
			.ToListAsync();

		return RequestResult<Order, Guid>.New(
			requestedObjects: order.Select(x => _dbLayerTranslator.Translate(x)),
			searchedIds: [id]);
	}

	public async Task<RequestResult<Order, Guid>> GetAllOrdersAsync()
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		var orders = await salesDbContext
			.Orders
			.Include(x => x.Products)
			.ToListAsync();

		return RequestResult<Order, Guid>.New(
			requestedObjects: orders.Select(x => _dbLayerTranslator.Translate(x)),
			searchedIds: orders.Select(x => x.Id));
	}

	public async Task<bool> ExistsOrderAsync(Guid id)
	{
		await using var salesDbContext = _dbContextFactory
			.CreateDbContext();

		return await salesDbContext
			.Orders
			.AnyAsync(x => x.Id == id);
	}
}