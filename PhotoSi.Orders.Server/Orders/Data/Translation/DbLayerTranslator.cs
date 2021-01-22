using System;
using System.Collections.Generic;
using System.Linq;
using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Data.Translation
{
	internal class DbLayerTranslator : IDbLayerTranslator
	{
		public OrderEntity Translate(Order source, IEnumerable<ProductEntity> existingProducts)
		{
			return new OrderEntity
			{
				Id = source.Id,
				Category = Translate(source.Category),
				Products = source.Products.Select(x => TranslateOrdered(x, existingProducts.First(e => e.Id == x.Id))),
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

		private static Category Translate(CategoryEntity source)
		{
			return new Category
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}

		private static Product Translate(OrderedProductEntity source)
		{
			var customOptions = source
				.CustomOptions
				.Select(Translate)
				.ToList();

			var customOptionsIds = customOptions
				.Select(o => o.Id);

			var originalFilteredOptions = source
				.ReferencedProduct
				.Options
				.Where(x => !customOptionsIds.Contains(x.Id))
				.Select(Translate);

			return new Product
			{
				Id = source.Id,
				Category = Translate(source.ReferencedProduct.Category),
				Options = customOptions.Union(originalFilteredOptions)
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

		private static Option Translate(OptionEntity source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
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

		private static Option Translate(OrderedOptionEntity source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.ReferencedOption.Name,
				Content = source.Content
			};
		}

		private static OrderedProductEntity TranslateOrdered(Product source, ProductEntity existingProduct)
		{
			var customOptions = source
				.Options
				.Where(x => existingProduct.Options.All(e => e.Id != x.Id && e.Content != x.Content))
				.Select(x => TranslateOrdered(x, source.Id));
			
			return new OrderedProductEntity
			{
				Id = source.Id,
				ProductId = source.Id,
				CustomOptions = customOptions
			};
		}

		private static OrderedOptionEntity TranslateOrdered(Option source, Guid productId)
		{
			return new OrderedOptionEntity
			{
				Id = source.Id,
				Content = source.Content,
				OptionId = source.Id,
				OrderedProductId = productId
			};
		}
	}
}