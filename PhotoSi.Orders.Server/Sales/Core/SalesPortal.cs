using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Sales.Core
{
	internal class SalesPortal : IOrdersEngine, ICheckGateway, IProductsPortal, IDemoPortal
	{
		private readonly ISalesRepository _repository;

		public SalesPortal(ISalesRepository repository)
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

		public Task<RequestResult<Product, Guid>> GetProductsAsync(IEnumerable<Guid> productsIds)
		{
			return _repository
				.GetProductsAsync(productsIds);
		}

		public Task<bool> ExistsCategoryAsync(Guid id)
		{
			return _repository
				.ExistsCategoryAsync(id);
		}

		public Task<bool> ExistsOrderAsync(Guid id)
		{
			return _repository
				.ExistsOrderAsync(id);
		}

		public Task UpsertAsync(IEnumerable<Category> categories)
		{
			return _repository
				.UpsertAsync(categories);
		}

		public Task UpsertAsync(IEnumerable<Product> categories)
		{
			return _repository
				.UpsertAsync(categories);
		}

		public Task<RequestResult<Product, Guid>> GetAllProductsAsync()
		{
			return _repository
				.GetAllProductsAsync();
		}
	}
}