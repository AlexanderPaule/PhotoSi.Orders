using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSi.Orders.Server.Sales.Core;

namespace PhotoSi.Orders.Server.Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DemoController : ControllerBase
	{
		private readonly IDemoDataCatalog _demoDataCatalog;
		private readonly ISalesPortal _salesPortal;
		private readonly IApiLayerTranslator _apiLayerTranslator;

		public DemoController(IDemoDataCatalog demoDataCatalog, ISalesPortal salesPortal, IApiLayerTranslator apiLayerTranslator)
		{
			_demoDataCatalog = demoDataCatalog;
			_salesPortal = salesPortal;
			_apiLayerTranslator = apiLayerTranslator;
		}

		[HttpPost("SetUp")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Setup()
		{
			var categories = _demoDataCatalog.GetCategories();
			var products = _demoDataCatalog.GetProducts();
			
			await _salesPortal.UpsertAsync(categories);
			await _salesPortal.UpsertAsync(products);
			
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
}