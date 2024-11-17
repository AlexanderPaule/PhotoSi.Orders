using PhotoSi.Products.Core.Models;
using PhotoSi.Products.Data.Models;

namespace PhotoSi.Products.Data.Translation;

internal class DbLayerTranslator : IDbLayerTranslator
{
	public Product Translate(ProductEntity source)
	{
		return new Product
		{
			Id = source.Id,
			Description = source.Description,
			Category = Translate(source.Category)
		};
	}

	public ProductEntity Translate(Product source)
	{
		return new ProductEntity
		{
			Id = source.Id,
			CategoryId = source.Category.Id,
			Description = source.Description
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

	public CategoryEntity Translate(Category source)
	{
		return new CategoryEntity
		{
			Id = source.Id,
			Name = source.Name,
			Description = source.Description
		};
	}
}