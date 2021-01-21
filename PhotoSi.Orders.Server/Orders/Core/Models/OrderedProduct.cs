using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Server.Orders.Core.Models
{
	public class OrderedProduct
	{
		public Guid Id { get; set; }
		public IEnumerable<Option> Options { get; set; }
	}
}