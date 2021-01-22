using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Orders.Data.Context;
using PhotoSi.Orders.Server.Orders.Data.Translation;

namespace PhotoSi.Orders.Server.Orders.Data
{
	internal class SalesPersistence : ISalesPersistence
	{
		private readonly IDbContextFactory _dbContextFactory;
		private readonly IDbLayerTranslator _dbLayerTranslator;

		public SalesPersistence(IDbContextFactory dbContextFactory, IDbLayerTranslator dbLayerTranslator)
		{
			_dbContextFactory = dbContextFactory;
			_dbLayerTranslator = dbLayerTranslator;
		}
		
		public async Task SaveAsync(Order order)
		{
			var entityOrder = _dbLayerTranslator.Translate(order);

			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			await salesDbContext.AddAsync(entityOrder);
			await salesDbContext.SaveChangesAsync();
		}

		public async Task<RequestResult<Order, Guid>> GetOrderAsync(Guid id)
		{
			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			var order = await salesDbContext
				.Orders
				.Include(x => x.Category)
				.Include(x => x.Products).ThenInclude(x => x.ReferencedProduct).ThenInclude(x => x.Category)
				.Include(x => x.Products).ThenInclude(x => x.ReferencedProduct).ThenInclude(x => x.Options)
				.Include(x => x.Products).ThenInclude(x => x.Options).ThenInclude(x => x.ReferencedOption)
				.Where(x => x.Id == id)
				.ToListAsync();
			
			return RequestResult<Order, Guid>.New(
				requestedObjects: order.Select(x => _dbLayerTranslator.Translate(x)),
				searchedIds: new [] { id });
		}

		public async Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> ids)
		{
			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			var order = await salesDbContext
				.Products
				.Include(x =>  x.Category)
				.Where(x => ids.Contains(x.Id))
				.ToListAsync();

			return RequestResult<Product, Guid>.New(
				requestedObjects: order.Select(x => _dbLayerTranslator.Translate(x)),
				searchedIds: ids);
		}

		public async Task<bool> ExistsCategoryAsync(Guid id)
		{
			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			return await salesDbContext
				.Categories
				.AnyAsync(x => x.Id == id);
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
}