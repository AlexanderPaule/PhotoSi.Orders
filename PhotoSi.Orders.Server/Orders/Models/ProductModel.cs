using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Orders.Models
{
	public class ProductModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public IEnumerable<OptionModel> CustomOptions { get; set; }
	}
}