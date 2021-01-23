using System;
using System.ComponentModel.DataAnnotations;
using PhotoSi.Orders.Server.Sales.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Models
{
	public class CategoryModel
	{
		[Required]
		public Guid Id { get; set; }
		[StringLength(CategoryEntity.NameLength)]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}