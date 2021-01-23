using System;
using System.ComponentModel.DataAnnotations;
using PhotoSi.Orders.Server.Sales.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
{
	public class OptionModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required, StringLength(OptionEntity.NameLength)]
		public string Name { get; set; }
		[Required]
		public string Content { get; set; }
	}
}