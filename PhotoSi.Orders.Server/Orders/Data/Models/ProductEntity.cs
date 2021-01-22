﻿using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class ProductEntity : TimeTrackedEntity
	{
		public Guid Id { get; set; }
		public IEnumerable<OptionEntity> Options { get; set; }

		public Guid CategoryId { get; set; }
		public CategoryEntity Category { get; set; }
		public IEnumerable<OrderedProductEntity> OrderedProducts { get; set; }
	}
}