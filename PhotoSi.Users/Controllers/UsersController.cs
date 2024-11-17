using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PhotoSi.Users.Controllers.Models;
using PhotoSi.Users.Core;

namespace PhotoSi.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly ILogger<UsersController> _logger;
	private readonly IUsersGateway _usersGateway;
	private readonly IApiLayerTranslator _apiLayerTranslator;
	private readonly IValidator _validator;

	public UsersController(ILogger<UsersController> logger, IUsersGateway productsPortal, IApiLayerTranslator apiLayerTranslator, IValidator validator)
	{
		_logger = logger;
		_usersGateway = productsPortal;
		_apiLayerTranslator = apiLayerTranslator;
		_validator = validator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody, Required] UserModel user)
	{
		_logger.LogInformation($"Product creation requested [{nameof(UserModel.Id)}:{user.Id}]");
		_logger.LogInformation($"Product validation start [{nameof(UserModel.Id)}:{user.Id}]");

		var validationResult = await _validator.ValidateAsync(user);
		if (!validationResult.IsValid)
		{
			_logger.LogInformation($"Order validation failed [{nameof(UserModel.Id)}:{user.Id}]");
			return BadRequest(validationResult.GetErrorMessage());
		}

		_logger.LogInformation($"Product validation end [{nameof(UserModel.Id)}:{user.Id}]");
		_logger.LogInformation($"Product process start [{nameof(UserModel.Id)}:{user.Id}]");

		await _usersGateway
			.UpsertAsync([_apiLayerTranslator.Translate(user)]);

		_logger.LogInformation($"Product process end [{nameof(UserModel.Id)}:{user.Id}]");

		return Ok(user);
	}

	[HttpGet("All")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll()
	{
		_logger.LogInformation("Order get all start");
		var requestResult = await _usersGateway.GetAllUsersAsync();
		_logger.LogInformation("Order get all end");

		var orders = requestResult
			.GetList()
			.Select(_apiLayerTranslator.Translate)
			.ToList();

		return Ok(orders);
	}
}