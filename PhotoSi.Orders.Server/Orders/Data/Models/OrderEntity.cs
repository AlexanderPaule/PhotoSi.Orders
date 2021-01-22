using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class OrderEntity : TimeTrackedEntity
	{
		public Guid Id { get; set; }
		public CategoryEntity Category { get; set; }
		public IEnumerable<ProductEntity> Products { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
	}
}