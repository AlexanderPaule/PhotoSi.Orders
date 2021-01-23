using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class ProductEntity : TimeTrackedEntity
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public IEnumerable<OptionEntity> Options { get; set; }

		[Required]
		public Guid CategoryId { get; set; }
		public CategoryEntity Category { get; set; }
		public IEnumerable<OrderedProductEntity> OrderedProducts { get; set; }
	}
}