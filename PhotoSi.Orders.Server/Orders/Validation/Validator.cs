using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoSi.Sales.Orders.Controllers;
using PhotoSi.Sales.Orders.Models;
using PhotoSi.Sales.Sales.Core;
using PhotoSi.Sales.Sales.Core.Models;
using PhotoSi.Sales.Services.Extensions;
using PhotoSi.Sales.Utils;

namespace PhotoSi.Sales.Orders.Validation
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
			
			if (order.OrderCategory.Id == Guid.Empty)
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.OrderCategory)}.{nameof(OrderCategoryModel.Id)} property is required");

			if (!order.Products.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)} Orders without products are not allowed");

			if (order.Products.Any(x => x.Id == Guid.Empty))
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.Id)} property is required");

			if (order.Products.SelectMany(x => x.CustomOptions).Any(x => x.Id == Guid.Empty))
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.CustomOptions)}.{nameof(Option.Id)} property is required");

			var productsWithDuplicatedOptions = GetProductsWithDuplicatedOptions(order.Products);
			
			if (productsWithDuplicatedOptions.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.CustomOptions)} products [{productsWithDuplicatedOptions.JoinStrings()}] contains duplicated options");

			if (!validationResult.IsValid)
				return validationResult;

			if (await _checkGateway.ExistsOrderAsync(order.Id))
			{
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} order with identifier [{order.OrderCategory.Id}] already exists");
				return validationResult;
			}

			if (!await _checkGateway.ExistsCategoryAsync(order.OrderCategory.Id))
			{
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.OrderCategory)}.{nameof(OrderCategoryModel.Id)} specified category does not exists [{order.OrderCategory.Id}] ");
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
				.Where(p => p.Category.Id != order.OrderCategory.Id)
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
				.Select(product => GetProductsWithMissingOptions(storedProducts, product))
				.Where(x => x.Item2.Any())
				.ToList()
				.ForEach(x => validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.CustomOptions)} product [{x.Item1}] has associated [{x.Item2.JoinStrings()}] options that does not exists"));
			
			return validationResult;
		}

		private static List<Guid> GetProductsWithDuplicatedOptions(IEnumerable<OrderedProductModel> products)
		{
			return products
				.Where(product =>
				{
					var duplicatedOptions = product
						.CustomOptions
						.Select(x => x.Id)
						.GroupBy(o => o)
						.Count(o => o.ToList().Count > 1);

					return duplicatedOptions > 0;
				})
				.Select(x => x.Id)
				.ToList();
		}

		private static (Guid, List<Guid>) GetProductsWithMissingOptions(RequestResult<Product, Guid> storedProducts, OrderedProductModel orderedProduct)
		{
			var storedProduct = storedProducts
				.GetList()
				.First(x => x.Id == orderedProduct.Id);

			var notExistingOptions = orderedProduct
				.CustomOptions
				.Select(x => x.Id)
				.Except(storedProduct.Options.Select(x => x.Id))
				.ToList();
			
			return (orderedProduct.Id, notExistingOptions);
		}
	}
}