using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Server.Orders.Controllers;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Controllers.Translation;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Models;

namespace PhotoSi.Orders.Server.Test.Orders
{
	[TestFixture]
	internal class OrdersControllerFixture
    {
	    private ILogger<OrdersController> _logger;

	    [SetUp]
	    public void SetUp()
	    {
		    _logger = Mock.Of<ILogger<OrdersController>>(MockBehavior.Loose);
	    }

	    [Test]
	    public async Task CreateSuccess()
	    {
		    var orderModel = new OrderModel();
		    var order = new Order();

		    var validator = new Mock<IValidator>(MockBehavior.Strict);
		    validator
			    .Setup(x => x.ValidateAsync(orderModel))
			    .ReturnsAsync(ValidationResult.New)
			    .Verifiable("Validation operation not performed");

		    var apiLayerTranslator = new Mock<IApiLayerTranslator>(MockBehavior.Strict);
		    apiLayerTranslator
			    .Setup(x => x.Translate(orderModel))
			    .Returns(order)
			    .Verifiable("Translation operation not performed");

		    var orderEngine = new Mock<IOrdersEngine>(MockBehavior.Strict);
		    orderEngine
			    .Setup(x => x.ProcessAsync(order))
			    .Returns(Task.CompletedTask)
			    .Verifiable("process operation not performed");
		    
		    var controller = new OrdersController(
			    logger: _logger,
			    validator: validator.Object,
			    apiLayerTranslator: apiLayerTranslator.Object,
			    ordersEngine: orderEngine.Object);


		    var result = await controller.Create(orderModel);


		    Assert.That(result, Is.TypeOf<OkObjectResult>());
		    validator.VerifyAll();
		    apiLayerTranslator.VerifyAll();
		    orderEngine.VerifyAll();
	    }

	    [Test]
	    public async Task CreateFailedValidation()
	    {
		    var orderModel = new OrderModel();
		    var validationResult = ValidationResult.New();
		    validationResult.AddErrorMessage<OrderModel>("Not Valid");

		    var validator = new Mock<IValidator>(MockBehavior.Strict);
		    validator
			    .Setup(x => x.ValidateAsync(orderModel))
			    .ReturnsAsync(validationResult)
			    .Verifiable("Validation operation not performed");

		    var controller = new OrdersController(
			    logger: _logger,
			    validator: validator.Object,
			    apiLayerTranslator: Mock.Of<IApiLayerTranslator>(MockBehavior.Strict),
			    ordersEngine: Mock.Of<IOrdersEngine>(MockBehavior.Strict));


			var result = await controller.Create(orderModel);


			Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
			validator.VerifyAll();
	    }

	    [Test]
	    public async Task GetFound()
	    {
		    var id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C");
			
		    var orderModel = new OrderModel();
			var order = new Order();
			
			var orderEngine = new Mock<IOrdersEngine>(MockBehavior.Strict);
			orderEngine
				.Setup(x => x.GetAsync(id))
				.ReturnsAsync(RequestResult<Order>.New(order))
				.Verifiable("Request operation not performed");

			var apiLayerTranslator = new Mock<IApiLayerTranslator>(MockBehavior.Strict);
			apiLayerTranslator
				.Setup(x => x.Translate(order))
				.Returns(orderModel)
				.Verifiable("Translation operation not performed");

			var controller = new OrdersController(
			    logger: _logger,
			    validator: Mock.Of<IValidator>(MockBehavior.Strict),
			    apiLayerTranslator: apiLayerTranslator.Object,
			    ordersEngine: orderEngine.Object);


			var result = await controller.Get(id);


			Assert.That(result, Is.TypeOf<OkObjectResult>());
			apiLayerTranslator.VerifyAll();
			orderEngine.VerifyAll();
	    }

	    [Test]
	    public async Task GetNotFound()
	    {
		    var id = new Guid("2B5174E4-37B7-44EE-A8A2-EE920C6FAB9C");
			
			var orderEngine = new Mock<IOrdersEngine>(MockBehavior.Strict);
			orderEngine
				.Setup(x => x.GetAsync(id))
				.ReturnsAsync(RequestResult<Order>.NewNotFound)
				.Verifiable("Request operation not performed");

			var controller = new OrdersController(
			    logger: _logger,
			    validator: Mock.Of<IValidator>(MockBehavior.Strict),
			    apiLayerTranslator: Mock.Of<IApiLayerTranslator>(MockBehavior.Strict),
			    ordersEngine: orderEngine.Object);


			var result = await controller.Get(id);


			Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
			orderEngine.VerifyAll();
	    }
    }
}
