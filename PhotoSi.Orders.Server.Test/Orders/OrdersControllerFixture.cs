using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PhotoSi.Orders.Server.Orders.Controllers;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
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

		    var orderEngine = new Mock<IOrderEngine>(MockBehavior.Strict);
		    orderEngine
			    .Setup(x => x.ProcessAsync(order))
			    .Returns(Task.CompletedTask)
			    .Verifiable("process operation not performed");
		    
		    var controller = new OrdersController(
			    logger: _logger,
			    validator: validator.Object,
			    apiLayerTranslator: apiLayerTranslator.Object,
			    orderEngine: orderEngine.Object);


		    var result = await controller.Create(orderModel);


		    Assert.That(result, Is.TypeOf<OkObjectResult>());
		    validator.VerifyAll();
		    apiLayerTranslator.VerifyAll();
		    orderEngine.VerifyAll();
	    }

	    [Test]
	    public async Task FailedValidation()
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
			    orderEngine: Mock.Of<IOrderEngine>(MockBehavior.Strict));


			var result = await controller.Create(orderModel);


			Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
			validator.VerifyAll();
	    }
    }
}
