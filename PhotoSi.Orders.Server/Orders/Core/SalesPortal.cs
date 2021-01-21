﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Core.Models;
using PhotoSi.Orders.Server.Orders.Data;

namespace PhotoSi.Orders.Server.Orders.Core
{
	internal class SalesPortal : IOrdersEngine, ICheckGateway
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
			return _persistence.GetProductsAsync(productsIds);
		}

		public Task<bool> ExistsCategoryAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}