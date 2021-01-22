using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class OrderedOptionEntity : TimeTrackedEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string Content { get; set; }

		public Guid OptionId { get; set; }
		public OptionEntity ReferencedOption { get; set; }
		public Guid OrderedProductId { get; set; }
		public OrderedProductEntity OrderedProduct { get; set; }
	}
}