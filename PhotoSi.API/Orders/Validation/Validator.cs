using PhotoSi.Addresses.Core;
using PhotoSi.API.Orders.Models;
using PhotoSi.API.Utils;
using PhotoSi.Orders.Core;
using PhotoSi.Products.Core;
using PhotoSi.Users.Core;

namespace PhotoSi.API.Orders.Validation;

internal class Validator : IValidator
{
	private readonly IAddressesGateway _addressGateway;
	private readonly IProductsGateway _productsGateway;
	private readonly IOrdersGateway _ordersGateway;
	private readonly IUsersGateway _usersGateway;

	public Validator(IAddressesGateway addressesGateway, IProductsGateway productsGateway, IOrdersGateway ordersGateway, IUsersGateway usersGateway)
	{
		_addressGateway = addressesGateway;
		_productsGateway = productsGateway;
		_ordersGateway = ordersGateway;
		_usersGateway = usersGateway;
	}

	public async Task<ValidationResult> ValidateAsync(OrderModel order)
	{
		var validationResult = ValidationResult.New();

		if (order.Id == Guid.Empty)
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} property is required");

		if (!validationResult.IsValid)
			return validationResult;

		if (await _ordersGateway.ExistsOrderAsync(order.Id))
		{
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} order with identifier [{order.Id}] already exists");
			return validationResult;
		}

		var productsIds = order
			.ProductsIds
			.Distinct()
			.ToList();

		var checkProducts = await _productsGateway
			.ExistsProductsAsync(productsIds);

		var missingProducts = checkProducts
			.Where(p => p.Value == false)
			.Select(x => x.Key)
			.ToList();

		if (missingProducts.Any())
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.ProductsIds)} [{missingProducts.JoinStrings()}] does not exists");

		if (!validationResult.IsValid)
			return validationResult;

		if (await _addressGateway.ExistsAddressAsync(order.AddressId))
		{
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.AddressId)} [{order.AddressId}] does not exists");
			return validationResult;
		}
		

		if (await _usersGateway.ExistsUserAsync(order.UserId))
		{
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.UserId)} [{order.UserId}] does not exists");
			return validationResult;
		}

		return validationResult;
	}
}