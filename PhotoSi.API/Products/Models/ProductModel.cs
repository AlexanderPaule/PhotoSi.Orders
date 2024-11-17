using System.ComponentModel.DataAnnotations;

namespace PhotoSi.API.Products.Models;

public class ProductModel
{
	[Required]
	public Guid Id { get; set; }
	[Required]
	public CategoryModel Category { get; set; }
	[Required]
	public string Description { get; set; }
}