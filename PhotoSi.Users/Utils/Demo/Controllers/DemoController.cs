using Microsoft.AspNetCore.Mvc;
using PhotoSi.Users.Core;

namespace PhotoSi.Users.Utils.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
	private readonly IDemoDataCatalog _demoDataCatalog;
	private readonly IDemoGateway _demoGateway;

	public DemoController(IDemoDataCatalog demoDataCatalog, IDemoGateway demoGateway)
	{
		_demoDataCatalog = demoDataCatalog;
		_demoGateway = demoGateway;
	}

	[HttpPost("SetUp")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> Setup()
	{
		var users = _demoDataCatalog.GetUsers();

		await _demoGateway.UpsertAsync(users);

		return Ok();
	}
}