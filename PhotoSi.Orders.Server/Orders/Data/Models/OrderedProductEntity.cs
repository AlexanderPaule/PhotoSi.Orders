using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class OrderedProductEntity : TimeTrackedEntity
	{
		[Key]
		public Guid Id { get; set; }
		public IEnumerable<OrderedOptionEntity> CustomOptions { get; set; }
		
		public Guid ProductId { get; set; }
		public ProductEntity ReferencedProduct { get; set; }
	}
}