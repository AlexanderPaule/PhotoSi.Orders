using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
{
	public class OptionModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
	}
}