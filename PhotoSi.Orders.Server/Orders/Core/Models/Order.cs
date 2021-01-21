using System;
using System.Collections.Generic;
using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Core.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public Category Category { get; set; }
		public IEnumerable<OrderedProduct> Products { get; set; }
	}
}