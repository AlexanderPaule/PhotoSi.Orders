using System;
using System.Collections.Generic;

namespace PhotoSi.Sales.Products.Models
{
	public class ProductModel
	{
		public Guid Id { get; set; }
		public CategoryModel Category { get; set; }
		public string Description { get; set; }
		public IEnumerable<OptionModel> Options { get; set; }
	}
}