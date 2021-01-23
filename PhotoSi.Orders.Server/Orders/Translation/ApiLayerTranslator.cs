using System.Linq;
using PhotoSi.Orders.Server.Orders.Controllers;
using PhotoSi.Orders.Server.Orders.Models;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Orders.Translation
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
				Category = Translate(source.Category),
				CreatedOn = source.CreatedOn,
				Products = source.Products.Select(TranslateOrdered)
			};
		}

		private static CategoryModel Translate(Category source)
		{
			return new CategoryModel
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}
		
		private static Product Translate(ProductModel source, Category category)
		{
			return new Product
			{
				Id = source.Id,
				Category = category,
				Options = source.CustomOptions.Select(Translate)
			};
		}

		private static Option Translate(OptionModel source)
		{
			return new Option
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
			};
		}

		private static ProductModel TranslateOrdered(Product source)
		{
			return new ProductModel
			{
				Id = source.Id,
				CustomOptions = source.Options.Select(Translate)
			};
		}

		private static OptionModel Translate(Option source)
		{
			return new OptionModel
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
			};
		}
	}
}