using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoSi.Orders.Controllers.Models;
using PhotoSi.Orders.Core;

namespace PhotoSi.Orders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
	private readonly ILogger<OrdersController> _logger;
	private readonly IValidator _validator;
	private readonly IApiLayerTranslator _apiLayerTranslator;
	private readonly IOrdersEngine _ordersEngine;

	public OrdersController(ILogger<OrdersController> logger, IValidator validator, IApiLayerTranslator apiLayerTranslator, IOrdersEngine ordersEngine)
	{
		_logger = logger;
		_validator = validator;
		_apiLayerTranslator = apiLayerTranslator;
		_ordersEngine = ordersEngine;
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
		{
			_logger.LogInformation($"Order validation failed [{nameof(OrderModel.Id)}:{order.Id}]");
			return BadRequest(validationResult.GetErrorMessage());
		}

		_logger.LogInformation($"Order validation end [{nameof(OrderModel.Id)}:{order.Id}]");
		_logger.LogInformation($"Order process start [{nameof(OrderModel.Id)}:{order.Id}]");

		await _ordersEngine
			.ProcessAsync(_apiLayerTranslator.Translate(order));

		_logger.LogInformation($"Order process end [{nameof(OrderModel.Id)}:{order.Id}]");

		return Ok(order);
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Get(Guid id)
	{
		_logger.LogInformation($"Order get start [{nameof(id)}:{id}]");

		var requestResult = await _ordersEngine.GetOrderAsync(id);
		if (!requestResult.FoundAll())
		{
			_logger.LogInformation($"Order not found [{nameof(id)}:{id}]");
			return NotFound(id);
		}

		_logger.LogInformation($"Order get end [{nameof(id)}:{id}]");
		return Ok(_apiLayerTranslator.Translate(requestResult.GetScalar()));
	}

	[HttpGet("All")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll()
	{
		_logger.LogInformation("Order get all start");
		var requestResult = await _ordersEngine.GetAllOrdersAsync();
		_logger.LogInformation("Order get all end");

		var orders = requestResult
			.GetList()
			.Select(_apiLayerTranslator.Translate)
			.ToList();

		return Ok(orders);
	}
}
