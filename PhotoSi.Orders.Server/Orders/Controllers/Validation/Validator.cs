using System;
using System.Linq;
using System.Threading.Tasks;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Core;
using PhotoSi.Orders.Server.Services.Extensions;

namespace PhotoSi.Orders.Server.Orders.Controllers.Validation
{
	internal class Validator : IValidator
	{
		private readonly IProductsStorage _productsStorage;

		public Validator(IProductsStorage productsStorage)
		{
			_productsStorage = productsStorage;
		}

		public async Task<ValidationResult> ValidateAsync(OrderModel order)
		{
			var validationResult = ValidationResult.New();

			if (order.Id == Guid.Empty)
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Id)} property is required");
			
			if (order.Category.Id == Guid.Empty)
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Category)}.{nameof(Category.Id)} property is required");
			
			if (order.Products.Any(x => x.Id == Guid.Empty))
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)}.{nameof(OrderedProductModel.Id)} property is required");

			if (!validationResult.IsValid)
				return validationResult;

			var productsIds = order
				.Products
				.Select(x => x.Id)
				.Distinct()
				.ToList();

			var storedProducts = await _productsStorage
				.GetProducts(productsIds)
				.ToListAsync();

			var wrongCategoryProducts = storedProducts
				.Where(p => p.Category.Id != order.Category.Id)
				.Select(x => x.Id)
				.ToList();

			if (wrongCategoryProducts.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)} [{wrongCategoryProducts.JoinStrings()}] has category different from the category specified in order");

			var missingProducts = productsIds
				.Except(storedProducts.Select(x => x.Id))
				.ToList();

			if (missingProducts.Any())
				validationResult.AddErrorMessage<Guid>($"{nameof(OrderModel)}.{nameof(OrderModel.Products)} [{missingProducts.JoinStrings()}] does not exists");

			return validationResult;
		}
	}
}