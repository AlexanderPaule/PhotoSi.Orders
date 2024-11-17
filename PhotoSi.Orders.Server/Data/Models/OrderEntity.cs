using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Data.Models;

internal class OrderEntity : TimeTrackedEntity
{
	public Guid Id { get; set; }
	public DateTimeOffset CreatedOn { get; set; }
	public IEnumerable<OrderedProductEntity> Products { get; set; }
}