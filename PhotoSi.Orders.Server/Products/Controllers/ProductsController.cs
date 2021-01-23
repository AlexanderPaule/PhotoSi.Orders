using System.ComponentModel.DataAnnotations;
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
		private readonly ISalesPortal _salesPortal;
		private readonly IApiLayerTranslator _apiLayerTranslator;
		private readonly IValidator _validator;

		public ProductsController(ILogger<ProductsController> logger, ISalesPortal salesPortal, IApiLayerTranslator apiLayerTranslator, IValidator validator)
		{
			_logger = logger;
			_salesPortal = salesPortal;
			_apiLayerTranslator = apiLayerTranslator;
			_validator = validator;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Upsert([FromBody, Required] ProductModel product)
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

			await _salesPortal
				.UpsertAsync(new [] { _apiLayerTranslator.Translate(product) });

			_logger.LogInformation($"Product process end [{nameof(ProductModel.Id)}:{product.Id}]");

			return Ok(product);
		}
	}
}