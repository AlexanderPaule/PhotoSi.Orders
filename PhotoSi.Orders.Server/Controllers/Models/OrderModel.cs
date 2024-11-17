﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Controllers.Models;

public class OrderModel
{
	[Required]
	public Guid Id { get; set; }
	[Required]
	public Guid AddressId { get; set; }
	[Required]
	public Guid UserId { get; set; }
	[Required]
	public IEnumerable<Guid> ProductsIds { get; set; }
	[Required]
	public DateTimeOffset CreatedOn { get; set; }
}