using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Core;
using PhotoSi.Orders.Core.Models;
using PhotoSi.Orders.Utils;
using PhotoSi.Sales.Products.Controllers;
using PhotoSi.Sales.Products.Models;

namespace PhotoSi.Sales.Test.Products
{
	[TestFixture]
	internal class ProductsControllerFixture
	{
		private ILogger<ProductsController> _logger;

		[SetUp]
		public void SetUp()
		{
			_logger = Mock.Of<ILogger<ProductsController>>(MockBehavior.Loose);
		}

		[Test]
		public async Task CreateSuccess()
		{
			var productModel = new ProductModel();
			var product = new Product();

			var validator = new Mock<IValidator>(MockBehavior.Strict);
			validator
				.Setup(x => x.ValidateAsync(productModel))
				.ReturnsAsync(ValidationResult.New)
				.Verifiable("Validation operation not performed");

			var apiLayerTranslator = new Mock<IApiLayerTranslator>(MockBehavior.Strict);
			apiLayerTranslator
				.Setup(x => x.Translate(productModel))
				.Returns(product)
				.Verifiable("Translation operation not performed");

			var productsPortal = new Mock<IProductsGateway>(MockBehavior.Strict);
			productsPortal
				.Setup(x => x.UpsertAsync(new[] { product }))
				.Returns(Task.CompletedTask)
				.Verifiable("process operation not performed");

			var controller = new ProductsController(
				logger: _logger,
				productsPortal: productsPortal.Object,
				apiLayerTranslator: apiLayerTranslator.Object,
				validator: validator.Object);


			var result = await controller.Create(productModel);


			Assert.That(result, Is.TypeOf<OkObjectResult>());
			validator.VerifyAll();
			apiLayerTranslator.VerifyAll();
			productsPortal.VerifyAll();
		}


		[Test]
		public async Task CreateFailedValidation()
		{
			var productModel = new ProductModel();
			var validationResult = ValidationResult.New();
			validationResult.AddErrorMessage<ProductModel>("Not Valid");

			var validator = new Mock<IValidator>(MockBehavior.Strict);
			validator
				.Setup(x => x.ValidateAsync(productModel))
				.ReturnsAsync(validationResult)
				.Verifiable("Validation operation not performed");

			var controller = new ProductsController(
				logger: _logger,
				productsPortal: Mock.Of<IProductsGateway>(MockBehavior.Strict),
				apiLayerTranslator: Mock.Of<IApiLayerTranslator>(MockBehavior.Strict),
				validator: validator.Object);


			var result = await controller.Create(productModel);


			Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
			validator.VerifyAll();
		}

		[Test]
		public async Task GetAll()
		{
			var product = new Product();
			var productModel = new ProductModel();
			var productsPortal = new Mock<IProductsGateway>(MockBehavior.Strict);
			productsPortal
				.Setup(x => x.GetAllProductsAsync())
				.ReturnsAsync(RequestResult<Product, Guid>.New(new[] { product }, new[] { new Guid() }))
				.Verifiable("Request operation not performed");

			var apiLayerTranslator = new Mock<IApiLayerTranslator>(MockBehavior.Strict);
			apiLayerTranslator
				.Setup(x => x.Translate(product))
				.Returns(productModel)
				.Verifiable("Translation operation not performed");

			var controller = new ProductsController(
				logger: _logger,
				productsPortal: productsPortal.Object,
				apiLayerTranslator: apiLayerTranslator.Object,
				validator: Mock.Of<IValidator>(MockBehavior.Strict));


			var result = await controller.GetAll();


			Assert.That(result, Is.TypeOf<OkObjectResult>());
			productsPortal.VerifyAll();
			apiLayerTranslator.VerifyAll();
		}
	}
}