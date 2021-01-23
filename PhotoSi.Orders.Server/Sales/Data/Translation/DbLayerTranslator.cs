using System;
using System.Linq;
using PhotoSi.Sales.Sales.Core.Models;
using PhotoSi.Sales.Sales.Data.Models;

namespace PhotoSi.Sales.Sales.Data.Translation
{
	internal class DbLayerTranslator : IDbLayerTranslator
	{
		public OrderEntity Translate(Order source)
		{
			return new OrderEntity
			{
				Id = source.Id,
				CategoryId = source.Category.Id,
				CreatedOn = source.CreatedOn
			};
		}

		public Order Translate(OrderEntity source)
		{
			return new Order
			{
				Id = source.Id,
				CreatedOn = source.CreatedOn,
				Category = Translate(source.Category),
				Products = source.Products.Select(Translate)
			};
		}

		public Product Translate(ProductEntity source)
		{
			return new Product
			{
				Id = source.Id,
				Description = source.Description,
				Category = Translate(source.Category),
				Options = source.Options.Select(Translate)
			};
		}

		public ProductEntity Translate(Product source)
		{
			return new ProductEntity
			{
				Id = source.Id,
				CategoryId = source.Category.Id,
				Description = source.Description
			};
		}

		public OrderedProductEntity TranslateOrdered(Product source, Guid orderId)
		{
			return new OrderedProductEntity
			{
				Id = Guid.NewGuid(),
				ProductId = source.Id,
				OrderId = orderId
			};
		}

		public OrderedOptionEntity TranslateOrdered(Option source, Guid productId)
		{
			return new OrderedOptionEntity
			{
				Id = Guid.NewGuid(),
				Content = source.Content,
				OptionId = source.Id,
				OrderedProductId = productId
			};
		}

		private static Category Translate(CategoryEntity source)
		{
			return new Category
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}

		public CategoryEntity Translate(Category source)
		{
			return new CategoryEntity
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}

		public OptionEntity Translate(Option source, Guid referencedProductId)
		{
			return new OptionEntity
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content,
				ProductId = referencedProductId
			};
		}

		private static Product Translate(OrderedProductEntity source)
		{
			var customOptions = source
				.CustomOptions
				.Select(Translate)
				.ToList();

			var customOptionsIds = source
				.CustomOptions
				.Select(o => o.OptionId);

			var originalFilteredOptions = source
				.ReferencedProduct
				.Options
				.Where(x => !customOptionsIds.Contains(x.Id))
				.Select(Translate);

			return new Product
			{
				Id = source.ReferencedProduct.Id,
				Category = Translate(source.ReferencedProduct.Category),
				Description = source.ReferencedProduct.Description,
				Options = customOptions.Union(originalFilteredOptions)
			};
		}

		private static Option Translate(OptionEntity source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
			};
		}

		private static Option Translate(OrderedOptionEntity source)
		{
			return new Option
			{
				Id = source.ReferencedOption.Id,
				Name = source.ReferencedOption.Name,
				Content = source.Content
			};
		}
	}
}