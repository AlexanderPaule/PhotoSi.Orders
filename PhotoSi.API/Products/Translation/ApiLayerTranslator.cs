using PhotoSi.API.Products.Models;
using PhotoSi.Products.Core.Models;

namespace PhotoSi.API.Products.Translation;

internal class ApiLayerTranslator : IApiLayerTranslator
{

	public ProductModel Translate(Product source)
	{
		return new ProductModel
		{
			Id = source.Id,
			Category = Translate(source.Category),
			Description = source.Description
		};
	}

	public Product Translate(ProductModel source)
	{
		return new Product
		{
			Id = source.Id,
			Category = Translate(source.Category),
			Description = source.Description
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
}