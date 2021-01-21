using System;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class ProductEntity
	{
		public Guid Id { get; set; }
		public CategoryEntity Category { get; set; }
	}
}