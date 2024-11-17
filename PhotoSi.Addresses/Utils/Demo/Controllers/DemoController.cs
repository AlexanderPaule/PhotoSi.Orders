using Microsoft.AspNetCore.Mvc;
using PhotoSi.Addresses.Core;

namespace PhotoSi.Addresses.Utils.Demo.Controllers;

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
		var addresses = _demoDataCatalog.GetAddresses();

		await _demoGateway.UpsertAsync(addresses);

		return Ok();
	}
}