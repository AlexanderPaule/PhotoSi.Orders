using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Sales.Data.Models
{
	internal class OptionEntity : TimeTrackedEntity
	{
		public const int NameLength = 100;

		[Key]
		public Guid Id { get; set; }
		[StringLength(NameLength)]
		public string Name { get; set; }
		public string Content { get; set; }

		[Required]
		public Guid ProductId { get; set; }
		public ProductEntity Product { get; set; }
		public IEnumerable<OrderedOptionEntity> CustomizedOptions { get; set; }
	}
}