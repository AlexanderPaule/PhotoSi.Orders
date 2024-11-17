using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoSi.Orders.Controllers.Models;
using PhotoSi.Orders.Core;
using PhotoSi.Orders.Core.Models;
using PhotoSi.Orders.Utils;

namespace PhotoSi.Orders.Controllers.Validation;

internal class Validator : IValidator
{
	private readonly ICheckGateway _checkGateway;

	public Validator(ICheckGateway checkGateway)
	{
		_checkGateway = checkGateway;
	}

	public async Task<ValidationResult> ValidateAsync(OrderModel order)
	{
		var validationResult = ValidationResult.New();

		if (order.Id == Guid.Empty)
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} property is required");

		if (!validationResult.IsValid)
			return validationResult;

		if (await _checkGateway.ExistsOrderAsync(order.Id))
		{
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} order with identifier [{order.Id}] already exists");
			return validationResult;
		}

		var productsIds = order
			.ProductsIds
			.Distinct()
			.ToList();

		var checkProducts = await _checkGateway
			.ExistsProductsAsync(productsIds);

		var missingProducts = checkProducts
			.Where(p => p.Value == false)
			.Select(x => x.Key)
			.ToList();

		if (missingProducts.Any())
			validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.ProductsIds)} [{missingProducts.JoinStrings()}] does not exists");

		if (!validationResult.IsValid)
			return validationResult;

		return validationResult;
	}
}