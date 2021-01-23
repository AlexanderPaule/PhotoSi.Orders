using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Sales.Sales.Data.Models
{
	internal class CategoryEntity : TimeTrackedEntity
	{
		public const int NameLength = 100;

		[Key]
		public Guid Id { get; set; }
		[StringLength(NameLength)]
		public string Name { get; set; }
		public string Description { get; set; }

		public IEnumerable<OrderEntity> Orders { get; set; }
		public IEnumerable<ProductEntity> Products { get; set; }
	}
}