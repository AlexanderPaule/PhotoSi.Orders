using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Sales.Data.Models
{
	internal class OrderedProductEntity : TimeTrackedEntity
	{
		[Key]
		public Guid Id { get; set; }
		public IEnumerable<OrderedOptionEntity> CustomOptions { get; set; }

		[Required]
		public Guid OrderId { get; set; }
		public OrderEntity ReferencedOrder { get; set; }
		[Required]
		public Guid ProductId { get; set; }
		public ProductEntity ReferencedProduct { get; set; }
	}
}