using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Orders.Models
{
	public class OrderedProductModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public IEnumerable<OrderedOptionModel> CustomOptions { get; set; }
	}
}