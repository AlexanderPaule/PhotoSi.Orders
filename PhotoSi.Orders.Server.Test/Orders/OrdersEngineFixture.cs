using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Models;
using PhotoSi.Orders.Server.Orders.Data;

namespace PhotoSi.Orders.Server.Test.Orders
{
	[TestFixture]
	internal class SalesPortalFixture
	{
		[Test]
		public async Task ProcessOrder()
		{
			var order = new Order();
			var persistence = new Mock<ISalesPersistence>(MockBehavior.Strict);
			persistence
				.Setup(x => x.SaveAsync(order))
				.Returns(Task.CompletedTask)
				.Verifiable("Save operation was not been performed");
			var ordersEngine = new SalesPortal(persistence.Object);

			await ordersEngine.ProcessAsync(order);
			
			persistence.Verify();
		}
		
		[Test]
		public async Task GetOrder()
		{
			var orderId = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C");
			var persistence = new Mock<ISalesPersistence>(MockBehavior.Strict);
			var order = new Order();
			persistence
				.Setup(x => x.GetOrderAsync(orderId))
				.ReturnsAsync(RequestResult<Order>.New(order))
				.Verifiable("Save operation was not been performed");
			var ordersEngine = new SalesPortal(persistence.Object);

			var requestResult = await ordersEngine.GetAsync(orderId);

			Assert.That(requestResult, Is.Not.Null);
			Assert.That(requestResult.Found, Is.True);
			Assert.That(requestResult.Object, Is.EqualTo(order));
			persistence.Verify();
		}
	}
}