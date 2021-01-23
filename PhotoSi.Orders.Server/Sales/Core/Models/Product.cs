using System;
using System.Collections.Generic;

namespace PhotoSi.Sales.Sales.Core.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
		public IEnumerable<Option> Options { get; set; }
	}
}