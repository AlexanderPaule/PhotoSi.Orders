using Microsoft.AspNetCore.Mvc;
using PhotoSi.Users.Controllers;
using PhotoSi.Users.Core;

namespace PhotoSi.Users.Utils.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
	private readonly IDemoDataCatalog _demoDataCatalog;
	private readonly IDemoGateway _demoGateway;
	private readonly IApiLayerTranslator _apiLayerTranslator;

	public DemoController(IDemoDataCatalog demoDataCatalog, IDemoGateway demoPortal, IApiLayerTranslator apiLayerTranslator)
	{
		_demoDataCatalog = demoDataCatalog;
		_demoGateway = demoPortal;
		_apiLayerTranslator = apiLayerTranslator;
	}

	[HttpPost("SetUp")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> Setup()
	{
		var users = _demoDataCatalog.GetUsers();

		await _demoGateway.UpsertAsync(users);

		return Ok();
	}

	[HttpGet("GetPrepared")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult GetConfiguredUsers()
	{
		var products = _demoDataCatalog
			.GetUsers()
			.Select(_apiLayerTranslator.Translate);

		return Ok(products);
	}
}