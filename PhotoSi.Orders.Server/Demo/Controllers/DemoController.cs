using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSi.Orders.Server.Demo.Controllers.Data;
using PhotoSi.Orders.Server.Orders.Core;

namespace PhotoSi.Orders.Server.Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DemoController : ControllerBase
	{
		private readonly IDemoDataCatalog _demoDataCatalog;
		private readonly ISalesCatalog _salesCatalog;

		public DemoController(IDemoDataCatalog demoDataCatalog, ISalesCatalog salesCatalog)
		{
			_demoDataCatalog = demoDataCatalog;
			_salesCatalog = salesCatalog;
		}

		[HttpPost("SetUp")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Setup()
		{
			var categories = _demoDataCatalog.GetCategories();
			var products = _demoDataCatalog.GetProducts();
			
			await _salesCatalog.UpsertAsync(categories);
			await _salesCatalog.UpsertAsync(products);
			
			return Ok();
		}
	}
}