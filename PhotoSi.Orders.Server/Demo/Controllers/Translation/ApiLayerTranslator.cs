using System.Linq;
using PhotoSi.Orders.Server.Demo.Controllers.Models;
using PhotoSi.Orders.Server.Sales.Core.Models;

namespace PhotoSi.Orders.Server.Demo.Controllers.Translation
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