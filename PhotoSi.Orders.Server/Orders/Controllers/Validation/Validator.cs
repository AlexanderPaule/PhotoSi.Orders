using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Services.Extensions;

namespace PhotoSi.Orders.Server.Orders.Controllers.Validation
{
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
			
			if (order.Category.Id == Guid.Empty)
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Category)}.{nameof(CategoryModel.Id)} property is required");

			if (!order.Products.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)} Orders without products are not allowed");

			if (order.Products.Any(x => x.Id == Guid.Empty))
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.Id)} property is required");

			if (order.Products.SelectMany(x => x.Options).Any(x => x.Id == Guid.Empty))
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.Id)} property is required");

			if (!validationResult.IsValid)
				return validationResult;

			if (await _checkGateway.ExistsOrderAsync(order.Id))
			{
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} order with identifier [{order.Category.Id}] already exists");
				return validationResult;
			}

			if (!await _checkGateway.ExistsCategoryAsync(order.Category.Id))
			{
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Category)}.{nameof(CategoryModel.Id)} specified category does not exists [{order.Category.Id}] ");
				return validationResult;
			}

			var productsIds = order
				.Products
				.Select(x => x.Id)
				.Distinct()
				.ToList();

			var storedProducts = await _checkGateway
				.GetProductsAsync(productsIds);

			var wrongCategoryProducts = storedProducts
				.GetList()
				.Where(p => p.Category.Id != order.Category.Id)
				.Select(x => x.Id)
				.ToList();

			if (wrongCategoryProducts.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)} [{wrongCategoryProducts.JoinStrings()}] has category different from the category specified in order");

			var missingProducts = productsIds
				.Except(storedProducts.GetList().Select(x => x.Id))
				.ToList();

			if (missingProducts.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)} [{missingProducts.JoinStrings()}] does not exists");

			if (!validationResult.IsValid)
				return validationResult;

			order.Products
				.ToList()
				.ForEach(product => ValidateOptions(storedProducts, product, validationResult));
			
			return validationResult;
		}

		private static void ValidateOptions(RequestResult<Product, Guid> storedProducts, OrderedProductModel product, ValidationResult validationResult)
		{
			var storedProduct = storedProducts
				.GetList()
				.First(x => x.Id == product.Id);

			var notExistingOptions = product
				.Options
				.Select(x => x.Id)
				.Except(storedProduct.Options.Select(x => x.Id))
				.ToList();

			if (notExistingOptions.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.Options)} product [{product.Id}] has associated [{notExistingOptions.JoinStrings()}] options that does not exists");
		}
	}
}