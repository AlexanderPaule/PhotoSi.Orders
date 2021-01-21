using System.ComponentModel.DataAnnotations;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
{
	public class OptionModel
	{
		[Required]
		[StringLength(OptionEntity.NameLength)]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
	}
}