using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
{
	public class OrderModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public CategoryModel Category { get; set; }
		[Required]
		public IEnumerable<OrderedProductModel> Products { get; set; }

		public OrderModel()
		{
			Products = new List<OrderedProductModel>();
		}
	}
}