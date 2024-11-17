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

	public UsersController(ILogger<UsersController> logger, IUsersGateway usersGateway, IApiLayerTranslator apiLayerTranslator, IValidator validator)
	{
		_logger = logger;
		_usersGateway = usersGateway;
		_apiLayerTranslator = apiLayerTranslator;
		_validator = validator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody, Required] UserModel user)
	{
		_logger.LogInformation($"User creation requested [{nameof(UserModel.Id)}:{user.Id}]");
		_logger.LogInformation($"User validation start [{nameof(UserModel.Id)}:{user.Id}]");

		var validationResult = await _validator.ValidateAsync(user);
		if (!validationResult.IsValid)
		{
			_logger.LogInformation($"User validation failed [{nameof(UserModel.Id)}:{user.Id}]");
			return BadRequest(validationResult.GetErrorMessage());
		}

		_logger.LogInformation($"User validation end [{nameof(UserModel.Id)}:{user.Id}]");
		_logger.LogInformation($"User process start [{nameof(UserModel.Id)}:{user.Id}]");

		await _usersGateway
			.UpsertAsync([_apiLayerTranslator.Translate(user)]);

		_logger.LogInformation($"User process end [{nameof(UserModel.Id)}:{user.Id}]");

		return Ok(user);
	}

	[HttpGet("All")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAll()
	{
		_logger.LogInformation("User get all start");
		var requestResult = await _usersGateway.GetAllUsersAsync();
		_logger.LogInformation("User get all end");

		var orders = requestResult
			.GetList()
			.Select(_apiLayerTranslator.Translate)
			.ToList();

		return Ok(orders);
	}
}