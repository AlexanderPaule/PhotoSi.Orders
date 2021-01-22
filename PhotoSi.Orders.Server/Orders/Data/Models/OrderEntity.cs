using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class OrderEntity : TimeTrackedEntity
	{
		public Guid Id { get; set; }
		public DateTimeOffset CreatedOn { get; set; }
		public IEnumerable<OrderedProductEntity> Products { get; set; }

		public Guid CategoryId { get; set; }
		public CategoryEntity Category { get; set; }
	}
}