using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Data.Models;

public class OrderEntity : TimeTrackedEntity
{
	public Guid Id { get; set; }
	public Guid AddressId { get; set; }
	public Guid UserId { get; set; }
	public IReadOnlyCollection<OrderedProductEntity> Products { get; set; }
	public DateTimeOffset CreatedOn { get; set; }
}