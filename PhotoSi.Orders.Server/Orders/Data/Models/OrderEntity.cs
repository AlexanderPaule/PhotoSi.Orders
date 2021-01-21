﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	public class OrderEntity
	{
		public Guid Id { get; set; }
		public CategoryEntity Category { get; set; }
		public IEnumerable<OrderedProductEntity> Products { get; set; }
	}
}