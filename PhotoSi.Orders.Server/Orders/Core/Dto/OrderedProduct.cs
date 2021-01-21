using System;
using System.Collections.Generic;

namespace PhotoSi.Orders.Server.Orders.Core.Dto
{
	public class OrderedProduct
	{
		public Guid Id { get; set; }
		public IEnumerable<Option> Options { get; set; }
	}
}