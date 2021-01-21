using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Controllers.Validation;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Test.Orders;

namespace PhotoSi.Orders.Server.Orders.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly ILogger<OrdersController> _logger;
		private readonly IValidator _validator;
		private readonly IApiLayerTranslator _apiLayerTranslator;
		private readonly IOrderEngine _orderEngine;

		public OrdersController(ILogger<OrdersController> logger, IValidator validator, IApiLayerTranslator apiLayerTranslator, IOrderEngine orderEngine)
		{
			_logger = logger;
			_validator = validator;
			_apiLayerTranslator = apiLayerTranslator;
			_orderEngine = orderEngine;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody, Required] OrderModel order)
		{
			_logger.LogInformation($"Order creation requested [{nameof(OrderModel.Id)}:{order.Id}]");
			_logger.LogInformation($"Order validation start [{nameof(OrderModel.Id)}:{order.Id}]");

			var validationResult = await _validator.ValidateAsync(order);
			if (!validationResult.IsValid)
				return BadRequest(validationResult.GetErrorMessage());

			_logger.LogInformation($"Order validation end [{nameof(OrderModel.Id)}:{order.Id}]");
			_logger.LogInformation($"Order process start [{nameof(OrderModel.Id)}:{order.Id}]");

			await _orderEngine
				.ProcessAsync(_apiLayerTranslator.Translate(order));

			_logger.LogInformation($"Order process end [{nameof(OrderModel.Id)}:{order.Id}]");

			return Ok(order);
		}
	}
}
