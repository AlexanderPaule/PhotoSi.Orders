using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Core.Models;

public record Order
{
	public Guid Id { get; set; }
	public Guid AddressId { get; set; }
	public Guid UserId { get; set; }
	public IEnumerable<Guid> ProductsIds { get; set; }
	public DateTimeOffset CreatedOn { get; set; }
}