﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Core
{
	internal class SalesPortal : IOrdersEngine, ICheckGateway, ISalesCatalog
	{
		private readonly ISalesPersistence _persistence;

		public SalesPortal(ISalesPersistence persistence)
		{
			_persistence = persistence;
		}

		public Task ProcessAsync(Order order)
		{
			return _persistence
				.SaveAsync(order);
		}

		public Task<RequestResult<Order, Guid>> GetAsync(Guid id)
		{
			return _persistence
				.GetOrderAsync(id);
		}

		public Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> productsIds)
		{
			return _persistence
				.GetProductsAsync(productsIds);
		}

		public Task<bool> ExistsCategoryAsync(Guid id)
		{
			return _persistence
				.ExistsCategoryAsync(id);
		}

		public Task<bool> ExistsOrderAsync(Guid id)
		{
			return _persistence
				.ExistsOrderAsync(id);
		}

		public Task UpsertAsync(IEnumerable<Category> categories)
		{
			return _persistence
				.Upsert(categories);
		}

		public Task UpsertAsync(IEnumerable<Product> categories)
		{
			return _persistence
				.Upsert(categories);
		}
	}
}