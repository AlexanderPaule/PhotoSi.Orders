using Moq;
using PhotoSi.Users.Core;
using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Tests;

[TestFixture]
internal class CoreEngineFixture
{
	private Mock<IUsersRepository> _usersRepositoryMock;

	[SetUp]
	public void SetUp()
	{
		_usersRepositoryMock = new Mock<IUsersRepository>(MockBehavior.Strict);
	}

	[Test]
	public async Task ExistsUsers()
	{
		var id = Guid.NewGuid();
		_usersRepositoryMock
			.Setup(x => x.ExistsUserAsync(id))
			.ReturnsAsync(true);

		var sut = new CoreEngine(_usersRepositoryMock.Object);


		var result = await sut.ExistsUserAsync(id);


		Assert.That(result, Is.EqualTo(true));
		_usersRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task UpsertUsers()
	{
		var addresses = new[] { new User() };
		_usersRepositoryMock
			.Setup(x => x.UpsertAsync(addresses))
			.Returns(Task.CompletedTask);

		var sut = new CoreEngine(_usersRepositoryMock.Object);


		await sut.UpsertAsync(addresses);


		_usersRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task GetAllUsers()
	{
		var expected = RequestResult<User, Guid>.New(requestedObjects: [new User()], searchedIds: [Guid.NewGuid()]);
		_usersRepositoryMock
			.Setup(x => x.GetAllUsersAsync())
			.ReturnsAsync(expected);

		var sut = new CoreEngine(_usersRepositoryMock.Object);


		var order = await sut.GetAllUsersAsync();


		Assert.That(order, Is.EqualTo(expected));
		_usersRepositoryMock.VerifyAll();
	}
}
