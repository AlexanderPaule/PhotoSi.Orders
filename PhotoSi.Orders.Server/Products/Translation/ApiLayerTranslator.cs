using System.Linq;
using PhotoSi.Sales.Products.Controllers;
using PhotoSi.Sales.Products.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Products.Translation
{
	internal class ApiLayerTranslator : IApiLayerTranslator
	{

		public ProductModel Translate(Product source)
		{
			return new ProductModel
			{
				Id = source.Id,
				Category = Translate(source.Category),
				Description = source.Description,
				Options = source.Options.Select(Translate)
			};
		}

		public Product Translate(ProductModel source)
		{
			return new Product
			{
				Id = source.Id,
				Category = Translate(source.Category),
				Description = source.Description,
				Options = source.Options.Select(Translate)
			};
		}

		private static Category Translate(CategoryModel source)
		{
			return new Category
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
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

		private static OptionModel Translate(Option source)
		{
			return new OptionModel
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
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
	}
}