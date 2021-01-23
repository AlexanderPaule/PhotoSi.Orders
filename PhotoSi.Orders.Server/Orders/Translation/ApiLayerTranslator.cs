using System.Linq;
using PhotoSi.Sales.Orders.Controllers;
using PhotoSi.Sales.Orders.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Orders.Translation
{
	public class ApiLayerTranslator : IApiLayerTranslator
	{
		public Order Translate(OrderModel source)
		{
			var category = new Category
			{
				Id = source.OrderCategory.Id,
				Name = source.OrderCategory.Name,
				Description = source.OrderCategory.Description
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
				OrderCategory = Translate(source.Category),
				CreatedOn = source.CreatedOn,
				Products = source.Products.Select(TranslateOrdered)
			};
		}

		private static OrderCategoryModel Translate(Category source)
		{
			return new OrderCategoryModel
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}
		
		private static Product Translate(OrderedProductModel source, Category category)
		{
			return new Product
			{
				Id = source.Id,
				Category = category,
				Options = source.CustomOptions.Select(Translate)
			};
		}

		private static Option Translate(OrderedOptionModel source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
			};
		}

		private static OrderedProductModel TranslateOrdered(Product source)
		{
			return new OrderedProductModel
			{
				Id = source.Id,
				CustomOptions = source.Options.Select(Translate)
			};
		}

		private static OrderedOptionModel Translate(Option source)
		{
			return new OrderedOptionModel
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
			};
		}
	}
}