using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoSi.Sales.Products.Models;
using PhotoSi.Sales.Sales.Core;

namespace PhotoSi.Sales.Products.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly ILogger<ProductsController> _logger;
		private readonly IProductsPortal _productsPortal;
		private readonly IApiLayerTranslator _apiLayerTranslator;
		private readonly IValidator _validator;

		public ProductsController(ILogger<ProductsController> logger, IProductsPortal productsPortal, IApiLayerTranslator apiLayerTranslator, IValidator validator)
		{
			_logger = logger;
			_productsPortal = productsPortal;
			_apiLayerTranslator = apiLayerTranslator;
			_validator = validator;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create([FromBody, Required] ProductModel product)
		{
			_logger.LogInformation($"Product creation requested [{nameof(ProductModel.Id)}:{product.Id}]");
			_logger.LogInformation($"Product validation start [{nameof(ProductModel.Id)}:{product.Id}]");

			var validationResult = await _validator.ValidateAsync(product);
			if (!validationResult.IsValid)
			{
				_logger.LogInformation($"Order validation failed [{nameof(ProductModel.Id)}:{product.Id}]");
				return BadRequest(validationResult.GetErrorMessage());
			}

			_logger.LogInformation($"Product validation end [{nameof(ProductModel.Id)}:{product.Id}]");
			_logger.LogInformation($"Product process start [{nameof(ProductModel.Id)}:{product.Id}]");

			await _productsPortal
				.UpsertAsync(new[] { _apiLayerTranslator.Translate(product) });

			_logger.LogInformation($"Product process end [{nameof(ProductModel.Id)}:{product.Id}]");

			return Ok(product);
		}

		[HttpGet("All")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			_logger.LogInformation("Order get all start");
			var requestResult = await _productsPortal.GetAllProductsAsync();
			_logger.LogInformation("Order get all end");

			var orders = requestResult
				.GetList()
				.Select(_apiLayerTranslator.Translate)
				.ToList();

			return Ok(orders);
		}
	}
}