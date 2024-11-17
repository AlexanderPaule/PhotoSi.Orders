using Microsoft.AspNetCore.Mvc;
using PhotoSi.Products.Core;

namespace PhotoSi.Products.Utils.Demo.Controllers;

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
		var categories = _demoDataCatalog.GetCategories();
		var products = _demoDataCatalog.GetProducts();

		await _demoGateway.UpsertAsync(categories);
		await _demoGateway.UpsertAsync(products);

		return Ok();
	}

	[HttpGet("Products")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult GetConfiguredProducts()
	{
		var products = _demoDataCatalog
			.GetProducts()
			.Select(_apiLayerTranslator.Translate);

		return Ok(products);
	}

	[HttpGet("Categories")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult GetConfiguredCategories()
	{
		var products = _demoDataCatalog
			.GetCategories()
			.Select(_apiLayerTranslator.Translate);

		return Ok(products);
	}
}