using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Server.Sales.Core;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Test.Sales
{
	[TestFixture]
	internal class SalesPortalFixture
	{
		[Test]
		public async Task ProcessOrder()
		{
			var order = new Order();
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.SaveAsync(order))
				.Returns(Task.CompletedTask)
				.Verifiable("Save operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			await ordersEngine.ProcessAsync(order);
			
			repository.Verify();
		}
		
		[Test]
		public async Task GetOrder()
		{
			var orderId = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C");
			var order = new Order();
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.GetOrderAsync(orderId))
				.ReturnsAsync(RequestResult<Order, Guid>.New(new [] { order }, new [] { orderId }))
				.Verifiable("Get operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			var requestResult = await ordersEngine.GetAsync(orderId);

			Assert.That(requestResult, Is.Not.Null);
			Assert.That(requestResult.FoundAll(), Is.True);
			Assert.That(requestResult.GetScalar(), Is.EqualTo(order));
			repository.Verify();
		}

		[Test]
		public async Task GetProducts()
		{
			var productsIds = new[]
			{
				new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C"),
				new Guid("3C5174E4-37B7-44EE-A8A2-EE920C6FAB9C")
			};
			var products = new[]
			{
				new Product()
			};
			var expectedRequestResult = RequestResult<Product, Guid>.New(products, productsIds);
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.GetProductsAsync(productsIds))
				.ReturnsAsync(expectedRequestResult)
				.Verifiable("Get operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			var actualRequestResult = await ordersEngine.GetProductsAsync(productsIds);

			Assert.That(actualRequestResult, Is.Not.Null);
			Assert.That(actualRequestResult, Is.EqualTo(expectedRequestResult));
			repository.Verify();
		}

		[Test]
		public async Task ExistsCategory()
		{
			var categoryId = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C");
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.ExistsCategoryAsync(categoryId))
				.ReturnsAsync(true)
				.Verifiable("Exists operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			var exists = await ordersEngine.ExistsCategoryAsync(categoryId);

			Assert.That(exists, Is.True);
			repository.Verify();
		}

		[Test]
		public async Task ExistsOrder()
		{
			var orderId = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C");
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.ExistsOrderAsync(orderId))
				.ReturnsAsync(true)
				.Verifiable("Exists operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			var exists = await ordersEngine.ExistsOrderAsync(orderId);

			Assert.That(exists, Is.True);
			repository.Verify();
		}

		[Test]
		public async Task UpsertCategories()
		{
			var categories = new[]
			{
				new Category()
			};
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.Upsert(categories))
				.Returns(Task.CompletedTask)
				.Verifiable("Upsert operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			await ordersEngine.UpsertAsync(categories);

			repository.Verify();
		}

		[Test]
		public async Task UpsertProducts()
		{
			var products = new[]
			{
				new Product()
			};
			var repository = new Mock<ISalesRepository>(MockBehavior.Strict);
			repository
				.Setup(x => x.Upsert(products))
				.Returns(Task.CompletedTask)
				.Verifiable("Upsert operation was not been performed");
			var ordersEngine = new SalesPortal(repository.Object);

			await ordersEngine.UpsertAsync(products);

			repository.Verify();
		}
	}
}