using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Data.Models;

public class OrderedProductEntity : TimeTrackedEntity
{
	[Key]
	public Guid Id { get; set; }
	[Required]
	public Guid OrderId { get; set; }
	public OrderEntity ReferencedOrder { get; set; }
	[Required]
	public Guid ProductId { get; set; }
}