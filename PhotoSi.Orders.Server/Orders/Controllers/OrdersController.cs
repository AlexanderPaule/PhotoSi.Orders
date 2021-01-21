using System;
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
			var validationResult = _validator.Validate(order);
			if (!validationResult.IsValid)
				return BadRequest(validationResult.GetErrorMessage());

			await _orderEngine
				.ProcessAsync(_apiLayerTranslator.Translate(order));

			return Ok(order);
		}
	}
}
