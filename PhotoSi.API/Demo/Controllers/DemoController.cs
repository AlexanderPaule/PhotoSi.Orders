using Microsoft.AspNetCore.Mvc;
using PhotoSi.Addresses.Core;
using PhotoSi.Products.Core;
using PhotoSi.Users.Core;

namespace PhotoSi.Addresses.Utils.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
	private readonly IDemoDataCatalog _demoDataCatalog;
	private readonly IAddressesGateway _addressesGateway;
	private readonly IUsersGateway _usersGateway;
	private readonly IProductsGateway _productsGateway;

	public DemoController(IDemoDataCatalog demoDataCatalog, IAddressesGateway addressesGateway, IUsersGateway usersGateway, IProductsGateway productsGateway)
	{
		_demoDataCatalog = demoDataCatalog;
		_addressesGateway = addressesGateway;
		_usersGateway = usersGateway;
		_productsGateway = productsGateway;
	}

	[HttpPost("SetUp")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> Setup()
	{
		var addresses = _demoDataCatalog.GetAddresses();
		var categories = _demoDataCatalog.GetCategories();
		var products = _demoDataCatalog.GetProducts();
		var users = _demoDataCatalog.GetUsers();


		await _productsGateway.UpsertAsync(categories);

		var tasks = new[]
		{
			_usersGateway.UpsertAsync(users),
			_productsGateway.UpsertAsync(products),
			_addressesGateway.UpsertAsync(addresses)
		};

		await Task.WhenAll(tasks);

		return Ok();
	}
}