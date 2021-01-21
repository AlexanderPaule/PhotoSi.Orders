using System.Linq;
using PhotoSi.Orders.Server.Orders.Core.Dto;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Data.Translation
{
	internal class DbLayerTranslator : IDbLayerTranslator
	{
		public Order Translate(OrderEntity source)
		{
			return new Order
			{
				Id = source.Id,
				Category = Translate(source.Category),
				Products = source.Products.Select(Translate)
			};
		}

		public OrderEntity Translate(Order source)
		{
			return new OrderEntity
			{
				Id = source.Id,
				Category = Translate(source.Category),
				Products = source.Products.Select(Translate)
			};
		}

		public Product Translate(ProductEntity source)
		{
			return new Product
			{
				Id = source.Id,
				Category = Translate(source.Category)
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

		private static CategoryEntity Translate(Category source)
		{
			return new CategoryEntity
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}

		private static OrderedProduct Translate(OrderedProductEntity source)
		{
			return new OrderedProduct
			{
				Id = source.Id,
				Options = source.Options.Select(Translate)
			};
		}

		private static Option Translate(OptionEntity source)
		{
			return new Option
			{
				Name = source.Name,
				Description = source.Description
			};
		}

		private static OrderedProductEntity Translate(OrderedProduct source)
		{
			return new OrderedProductEntity
			{
				Id = source.Id,
				Options = source.Options.Select(Translate)
			};
		}

		private static OptionEntity Translate(Option source)
		{
			return new OptionEntity
			{
				Name = source.Name,
				Description = source.Description
			};
		}
	}
}