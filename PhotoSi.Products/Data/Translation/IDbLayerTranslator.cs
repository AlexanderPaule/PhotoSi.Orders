using PhotoSi.Products.Core.Models;
using PhotoSi.Products.Data.Models;

namespace PhotoSi.Products.Data.Translation;

internal interface IDbLayerTranslator
{
	Product Translate(ProductEntity source);
	ProductEntity Translate(Product source);
	CategoryEntity Translate(Category source);
	OptionEntity Translate(Option source, Guid referencedProductId);
}