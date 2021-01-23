using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Products.Models
{
	public class ProductModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public CategoryModel Category { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public IEnumerable<OptionModel> Options { get; set; }
	}
}