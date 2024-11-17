using Microsoft.AspNetCore.Mvc;
using PhotoSi.Products.Controllers;
using PhotoSi.Products.Core;

namespace PhotoSi.Products.Utils.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
	private readonly IDemoDataCatalog _demoDataCatalog;
	private readonly IDemoGateway _demoGateway;

	public DemoController(IDemoDataCatalog demoDataCatalog, IDemoGateway demoPortal)
	{
		_demoDataCatalog = demoDataCatalog;
		_demoGateway = demoPortal;
	}

	[HttpPost("SetUp")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> Setup()
	{
		var categories = _demoDataCatalog.GetCategories();
		var products = _demoDataCatalog.GetProducts();

		await _demoGateway.UpsertAsync(categories);
		await _demoGateway.UpsertAsync(products);

		return Ok();
	}
}