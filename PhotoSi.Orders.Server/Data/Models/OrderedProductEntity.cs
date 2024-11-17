using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Data.Models;

public class OrderedProductEntity : TimeTrackedEntity
{
	public Guid Id { get; set; }
	[Required]
	public Guid OrderId { get; set; }
	public OrderEntity Order { get; set; }
	[Required]
	public Guid ProductId { get; set; }
}