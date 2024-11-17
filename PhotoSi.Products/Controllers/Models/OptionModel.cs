using System.ComponentModel.DataAnnotations;
using PhotoSi.Products.Data.Models;

namespace PhotoSi.Products.Controllers.Models;

public class OptionModel
{
	[Required]
	public Guid Id { get; set; }
	[Required, StringLength(OptionEntity.NameLength)]
	public string Name { get; set; }
	[Required]
	public string Content { get; set; }
}