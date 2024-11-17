using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PhotoSi.Addresses.Core;
using PhotoSi.API.Addresses.Models;

namespace PhotoSi.API.Addresses;

[ApiController]
[Route("[controller]")]
public class AddressesController : ControllerBase
{
	private readonly ILogger<AddressesController> _logger;
	private readonly IAddressesGateway _usersGateway;
	private readonly IApiLayerTranslator _apiLayerTranslator;
	private readonly IValidator _validator;

	public AddressesController(ILogger<AddressesController> logger, IAddressesGateway productsPortal, IApiLayerTranslator apiLayerTranslator, IValidator validator)
	{
		_logger = logger;
		_usersGateway = productsPortal;
		_apiLayerTranslator = apiLayerTranslator;
		_validator = validator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody, Required] AddrressModel user)
	{
		_logger.LogInformation($"Address creation requested [{nameof(AddrressModel.Id)}:{user.Id}]");
		_logger.LogInformation($"Address validation start [{nameof(AddrressModel.Id)}:{user.Id}]");

		var validationResult = _validator.Validate(user);
		if (!validationResult.IsValid)
		{
			_logger.LogInformation($"Address validation failed [{nameof(AddrressModel.Id)}:{user.Id}]");
			return BadRequest(validationResult.GetErrorMessage());
		}

		_logger.LogInformation($"Address validation end [{nameof(AddrressModel.Id)}:{user.Id}]");
		_logger.LogInformation($"Address process start [{nameof(AddrressModel.Id)}:{user.Id}]");

		await _usersGateway
			.UpsertAsync([_apiLayerTranslator.Translate(user)]);

		_logger.LogInformation($"Address process end [{nameof(AddrressModel.Id)}:{user.Id}]");

		return Ok(user);
	}

	[HttpGet("All")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll()
	{
		_logger.LogInformation("Address get all start");
		var requestResult = await _usersGateway.GetAllAddressesAsync();
		_logger.LogInformation("Address get all end");

		var orders = requestResult
			.GetList()
			.Select(_apiLayerTranslator.Translate)
			.ToList();

		return Ok(orders);
	}
}