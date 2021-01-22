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
				Products = source.Products.Select(x => Translate(x, existingProducts.First(e => e.Id == x.Id))),
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
				Category = Translate(source.Category),
				Options = source.Options.Select(Translate)
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
				.Options
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

		private static CategoryEntity Translate(Category source)
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

		private static Option Translate(OrderedOptionEntity source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.ReferencedOption.Name,
				Content = source.Content
			};
		}

		private static OrderedProductEntity Translate(Product source, ProductEntity existingProduct)
		{
			var orderedOptionEntities = source
				.Options
				.Where(x => existingProduct.Options.All(e => e.Id != x.Id && e.Content != x.Content))
				.Select(x => Translate(x, source.Id));
			
			return new OrderedProductEntity
			{
				Id = source.Id,
				ProductId = source.Id,
				Options = orderedOptionEntities
			};
		}

		private static OrderedOptionEntity Translate(Option source, Guid productId)
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