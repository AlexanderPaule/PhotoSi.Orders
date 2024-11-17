using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Products.Data.Models;

public class ProductEntity : TimeTrackedEntity
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	[Required]
	public Guid CategoryId { get; set; }
	public CategoryEntity Category { get; set; }
}