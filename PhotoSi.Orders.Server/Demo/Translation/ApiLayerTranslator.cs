using System.Linq;
using PhotoSi.Sales.Demo.Controllers;
using PhotoSi.Sales.Demo.Models;
using PhotoSi.Sales.Sales.Core.Models;

namespace PhotoSi.Sales.Demo.Translation
{
	internal class ApiLayerTranslator : IApiLayerTranslator
	{

		public DemoProductModel Translate(Product source)
		{
			return new DemoProductModel
			{
				Id = source.Id,
				DemoCategory = Translate(source.Category),
				Description = source.Description,
				Options = source.Options.Select(Translate)
			};
		}

		public DemoCategoryModel Translate(Category source)
		{
			return new DemoCategoryModel
			{
				Id = source.Id,
				Name = source.Name,
				Description = source.Description
			};
		}

		private static DemoOptionModel Translate(Option source)
		{
			return new DemoOptionModel
			{
				Id = source.Id,
				Name = source.Name,
				Content = source.Content
			};
		}
	}
}