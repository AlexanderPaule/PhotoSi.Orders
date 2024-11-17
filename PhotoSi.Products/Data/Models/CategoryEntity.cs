using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Products.Data.Models;

public class CategoryEntity : TimeTrackedEntity
{
	public const int NameLength = 100;

	public Guid Id { get; set; }
	[StringLength(NameLength)]
	public string Name { get; set; }
	public string Description { get; set; }
	public IEnumerable<ProductEntity> Products { get; set; }
}