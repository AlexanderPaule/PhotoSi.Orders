using System.Linq;
using PhotoSi.Orders.Server.Orders.Controllers.Models;
using PhotoSi.Orders.Server.Orders.Core.Dto;

namespace PhotoSi.Orders.Server.Orders.Controllers.Translation
{
	public class ApiLayerTranslator : IApiLayerTranslator
	{
		public Order Translate(OrderModel source)
		{
			var category = new Category
			{
				Id = source.Category.Id,
				Name = source.Category.Name,
				Description = source.Category.Description
			};
			
			return new Order
			{
				Id = source.Id,
				Category = category,
				CreatedOn = source.CreatedOn,
				Products = source.Products.Select(x => Translate(x, category))
			};
		}

		public OrderModel Translate(Order source)
		{
			return new OrderModel
			{
				Id = source.Id,
				Category = new CategoryModel
				{
					Id = source.Category.Id,
					Name = source.Category.Name,
					Description = source.Category.Description
				},
				Products = source.Products.Select(Translate)
			};
		}

		private static Product Translate(OrderedProductModel source, Category category)
		{
			return new Product
			{
				Id = source.Id,
				Category = category,
				Options = source.Options.Select(Translate)
			};
		}

		private static Option Translate(OptionModel source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}

		private static OrderedProductModel Translate(Product source)
		{
			return new OrderedProductModel
			{
				Id = source.Id,
				Options = source.Options.Select(Translate)
			};
		}

		private static OptionModel Translate(Option source)
		{
			return new OptionModel
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}
	}
}