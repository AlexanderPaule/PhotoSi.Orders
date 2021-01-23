using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Orders.Models
{
	public class OrderModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public DateTimeOffset CreatedOn { get; set; }
		[Required]
		public OrderCategoryModel OrderCategory { get; set; }
		[Required]
		public IEnumerable<OrderedProductModel> Products { get; set; }
	}
}