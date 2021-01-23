using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSi.Sales.Sales.Core;
using PhotoSi.Sales.Sales.Core.Models;
using PhotoSi.Sales.Sales.Data.Context;
using PhotoSi.Sales.Sales.Data.Models;
using PhotoSi.Sales.Sales.Data.Translation;

namespace PhotoSi.Sales.Sales.Data
{
	internal class SalesRepository : ISalesRepository
	{
		private readonly IDbContextFactory _dbContextFactory;
		private readonly IDbLayerTranslator _dbLayerTranslator;

		public SalesRepository(IDbContextFactory dbContextFactory, IDbLayerTranslator dbLayerTranslator)
		{
			_dbContextFactory = dbContextFactory;
			_dbLayerTranslator = dbLayerTranslator;
		}
		
		public async Task SaveAsync(Order order)
		{
			var existingProductTask = GetProductEntities(order.Products.Select(x => x.Id));
			
			var entityOrder = _dbLayerTranslator
				.Translate(order);

			var orderedProducts = new List<OrderedProductEntity>();
			var orderedCustomOptions = new List<OrderedOptionEntity>();

			var existingProducts = await existingProductTask;
			
			foreach (var product in order.Products)
			{
				var orderedProduct = _dbLayerTranslator
					.TranslateOrdered(product, order.Id);
				
				var referencedProduct = existingProducts
					.First(e => e.Id == product.Id);

				var customOptions = product
					.Options
					.Select(o => new
					{
						OrderProductId = orderedProduct.Id,
						OrderOption = o,
						OriginalOptions = referencedProduct.Options
					})
					.Where(x => x.OriginalOptions.Any(o => o.Id == x.OrderOption.Id && o.Content != x.OrderOption.Content))
					.Select(x => _dbLayerTranslator.TranslateOrdered(x.OrderOption, x.OrderProductId));

				orderedProducts.Add(orderedProduct);
				orderedCustomOptions.AddRange(customOptions);
			}

			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			await salesDbContext.AddAsync(entityOrder);
			await salesDbContext.AddRangeAsync(orderedProducts);
			await salesDbContext.AddRangeAsync(orderedCustomOptions);
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
				.Include(x => x.Products).ThenInclude(x => x.CustomOptions).ThenInclude(x => x.ReferencedOption)
				.Where(x => x.Id == id)
				.ToListAsync();
			
			return RequestResult<Order, Guid>.New(
				requestedObjects: order.Select(x => _dbLayerTranslator.Translate(x)),
				searchedIds: new [] { id });
		}

		public async Task<RequestResult<Order, Guid>> GetAllOrdersAsync()
		{
			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			var orders = await salesDbContext
				.Orders
				.Include(x => x.Category)
				.Include(x => x.Products).ThenInclude(x => x.ReferencedProduct).ThenInclude(x => x.Category)
				.Include(x => x.Products).ThenInclude(x => x.ReferencedProduct).ThenInclude(x => x.Options)
				.Include(x => x.Products).ThenInclude(x => x.CustomOptions).ThenInclude(x => x.ReferencedOption)
				.ToListAsync();

			return RequestResult<Order, Guid>.New(
				requestedObjects: orders.Select(x => _dbLayerTranslator.Translate(x)),
				searchedIds: orders.Select(x => x.Id));
		}

		public async Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> ids)
		{
			var productEntities = await GetProductEntities(ids);

			return RequestResult<Product, Guid>.New(
				requestedObjects: productEntities.Select(x => _dbLayerTranslator.Translate(x)),
				searchedIds: ids);
		}

		public async Task<RequestResult<Product, Guid>> GetAllProductsAsync()
		{
			var productEntities = await GetProductEntities(Enumerable.Empty<Guid>());

			return RequestResult<Product, Guid>.New(
				requestedObjects: productEntities.Select(x => _dbLayerTranslator.Translate(x)),
				searchedIds: productEntities.Select(x => x.Id));
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

		public async Task UpsertAsync(IEnumerable<Category> categories)
		{
			var categoryEntities = categories
				.Select(_dbLayerTranslator.Translate)
				.ToList();
			
			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			await salesDbContext.Categories.UpsertBulkAsync(
				entities: categoryEntities,
				dbFilter: e => categoryEntities.Select(x => x.Id).Contains(e.Id));
			
			await salesDbContext.SaveChangesAsync();
		}

		public async Task UpsertAsync(IEnumerable<Product> products)
		{
			var productEntities = products
				.Select(_dbLayerTranslator.Translate)
				.ToList();

			var optionEntities = products
				.SelectMany(x => x.Options.Select(o => _dbLayerTranslator.Translate(o, x.Id)))
				.ToList();

			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			await salesDbContext.Products.UpsertBulkAsync(
				entities: productEntities,
				dbFilter: e => productEntities.Select(x => x.Id).Contains(e.Id));

			await salesDbContext.Options.UpsertBulkAsync(
				entities: optionEntities,
				dbFilter: e => optionEntities.Select(x => x.Id).Contains(e.Id));

			await salesDbContext.SaveChangesAsync();
		}

		private async Task<List<ProductEntity>> GetProductEntities(IEnumerable<Guid> ids)
		{
			await using var salesDbContext = _dbContextFactory
				.CreateDbContext();

			return await salesDbContext
				.Products
				.Include(x => x.Category)
				.Include(x => x.Options)
				.Where(x => !ids.Any() || ids.Contains(x.Id))
				.ToListAsync();
		}
	}
}