using Moq;
using PhotoSi.Addresses.Core;
using PhotoSi.Addresses.Core.Models;

namespace PhotoSi.Addresses.Tests;

[TestFixture]
internal class CoreEngineFixture
{
	private Mock<IAddressesRepository> _addressesRepositoryMock;

	[SetUp]
	public void SetUp()
	{
		_addressesRepositoryMock = new Mock<IAddressesRepository>(MockBehavior.Strict);
	}

	[Test]
	public async Task ExistsAddress()
	{
		var id = Guid.NewGuid();
		_addressesRepositoryMock
			.Setup(x => x.ExistsAddressAsync(id))
			.ReturnsAsync(true);

		var sut = new CoreEngine(_addressesRepositoryMock.Object);


		var result = await sut.ExistsAddressAsync(id);


		Assert.That(result, Is.EqualTo(true));
		_addressesRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task Upsert()
	{
		var addresses = new[] { new Address() };
		_addressesRepositoryMock
			.Setup(x => x.UpsertAsync(addresses))
			.Returns(Task.CompletedTask);

		var sut = new CoreEngine(_addressesRepositoryMock.Object);


		await sut.UpsertAsync(addresses);


		_addressesRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task GetAllAddresses()
	{
		var expected = RequestResult<Address, Guid>.New(requestedObjects: [new Address()], searchedIds: [Guid.NewGuid()]);
		_addressesRepositoryMock
			.Setup(x => x.GetAllAddressesAsync())
			.ReturnsAsync(expected);

		var sut = new CoreEngine(_addressesRepositoryMock.Object);


		var order = await sut.GetAllAddressesAsync();


		Assert.That(order, Is.EqualTo(expected));
		_addressesRepositoryMock.VerifyAll();
	}
}
