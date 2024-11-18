using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Core;
using PhotoSi.Orders.Core.Models;

namespace PhotoSi.Orders.Tests;

[TestFixture]
internal class CoreEngineFixture
{
	private Mock<IOrdersRepository> _orderRepositoryMock;

	[SetUp]
	public void SetUp()
	{
		_orderRepositoryMock = new Mock<IOrdersRepository>(MockBehavior.Strict);
	}

	[Test]
	public async Task ExistsOrder()
	{
		var orderId = Guid.NewGuid();
		_orderRepositoryMock
			.Setup(x => x.ExistsOrderAsync(orderId))
			.ReturnsAsync(true);

		var sut = new CoreEngine(_orderRepositoryMock.Object);


		var result = await sut.ExistsOrderAsync(orderId);


		Assert.That(result, Is.EqualTo(true));
		_orderRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task Process()
	{
		var order = new Order();
		_orderRepositoryMock
			.Setup(x => x.SaveAsync(order))
			.Returns(Task.CompletedTask);

		var sut = new CoreEngine(_orderRepositoryMock.Object);


		await sut.ProcessAsync(order);


		_orderRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task GetOrder()
	{
		var orderId = Guid.NewGuid();
		var expected = RequestResult<Order, Guid>.New(requestedObjects: [new Order()], searchedIds: [orderId]);
		_orderRepositoryMock
			.Setup(x => x.GetOrderAsync(orderId))
			.ReturnsAsync(expected);

		var sut = new CoreEngine(_orderRepositoryMock.Object);


		var order = await sut.GetOrderAsync(orderId);


		Assert.That(order, Is.EqualTo(expected));
		_orderRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task GetAllOrders()
	{
		var expected = RequestResult<Order, Guid>.New(requestedObjects: [new Order()], searchedIds: [Guid.NewGuid()]);
		_orderRepositoryMock
			.Setup(x => x.GetAllOrdersAsync())
			.ReturnsAsync(expected);

		var sut = new CoreEngine(_orderRepositoryMock.Object);


		var order = await sut.GetAllOrdersAsync();


		Assert.That(order, Is.EqualTo(expected));
		_orderRepositoryMock.VerifyAll();
	}
}
