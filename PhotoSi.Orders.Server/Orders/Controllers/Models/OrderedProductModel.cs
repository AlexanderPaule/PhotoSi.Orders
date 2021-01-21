using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
{
	public class OrderedProductModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public IEnumerable<OptionModel> Options { get; set; }
	}
}