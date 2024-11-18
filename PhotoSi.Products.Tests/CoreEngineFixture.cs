using Moq;
using PhotoSi.Products.Core;
using PhotoSi.Products.Core.Models;

namespace PhotoSi.Products.Tests;

[TestFixture]
internal class CoreEngineFixture
{
	private Mock<IProductsRepository> _productsRepositoryMock;

	[SetUp]
	public void SetUp()
	{
		_productsRepositoryMock = new Mock<IProductsRepository>(MockBehavior.Strict);
	}

	[Test]
	public async Task ExistsCategory()
	{
		var id = Guid.NewGuid();
		_productsRepositoryMock
			.Setup(x => x.ExistsCategoryAsync(id))
			.ReturnsAsync(true);

		var sut = new CoreEngine(_productsRepositoryMock.Object);


		var result = await sut.ExistsCategoryAsync(id);


		Assert.That(result, Is.EqualTo(true));
		_productsRepositoryMock.VerifyAll();
	}


	[Test]
	public async Task ExistsProduct()
	{
		var ids = new[] { Guid.NewGuid() };
		var expected = new Dictionary<Guid, bool> { [Guid.NewGuid()] = true };
		_productsRepositoryMock
			.Setup(x => x.ExistsProductsAsync(ids))
			.ReturnsAsync(expected);

		var sut = new CoreEngine(_productsRepositoryMock.Object);


		var result = await sut.ExistsProductsAsync(ids);


		Assert.That(result, Is.EqualTo(expected));
		_productsRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task UpsertCategories()
	{
		var categories = new[] { new Category() };
		_productsRepositoryMock
			.Setup(x => x.UpsertAsync(categories))
			.Returns(Task.CompletedTask);

		var sut = new CoreEngine(_productsRepositoryMock.Object);


		await sut.UpsertAsync(categories);


		_productsRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task UpsertProducts()
	{
		var products = new[] { new Product() };
		_productsRepositoryMock
			.Setup(x => x.UpsertAsync(products))
			.Returns(Task.CompletedTask);

		var sut = new CoreEngine(_productsRepositoryMock.Object);


		await sut.UpsertAsync(products);


		_productsRepositoryMock.VerifyAll();
	}

	[Test]
	public async Task GetAllProducts()
	{
		var expected = RequestResult<Product, Guid>.New(requestedObjects: [new Product()], searchedIds: [Guid.NewGuid()]);
		_productsRepositoryMock
			.Setup(x => x.GetAllProductsAsync())
			.ReturnsAsync(expected);

		var sut = new CoreEngine(_productsRepositoryMock.Object);


		var order = await sut.GetAllProductsAsync();


		Assert.That(order, Is.EqualTo(expected));
		_productsRepositoryMock.VerifyAll();
	}
}
