using System.Linq;
using PhotoSi.Sales.Demo.Controllers;
using PhotoSi.Sales.Demo.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Demo.Translation
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

		public CategoryModel Translate(Category source)
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
	}
}