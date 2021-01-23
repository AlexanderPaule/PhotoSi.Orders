using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Sales.Data.Models
{
	internal class OrderEntity : TimeTrackedEntity
	{
		public Guid Id { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
		public IEnumerable<OrderedProductEntity> Products { get; set; }

		[Required]
		public Guid CategoryId { get; set; }
		public CategoryEntity Category { get; set; }
	}
}