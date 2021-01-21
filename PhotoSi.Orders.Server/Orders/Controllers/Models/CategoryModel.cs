using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
{
	public class CategoryModel
	{
		[Required]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}