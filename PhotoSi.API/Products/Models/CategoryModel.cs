using System.ComponentModel.DataAnnotations;
using PhotoSi.Products.Data.Models;

namespace PhotoSi.API.Products.Models;

public class CategoryModel
{
	[Required]
	public Guid Id { get; set; }
	[StringLength(CategoryEntity.NameLength)]
	public string Name { get; set; }
	public string Description { get; set; }
}