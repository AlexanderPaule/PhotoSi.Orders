using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	internal class CategoryEntity : TimeTrackedEntity
	{
		public const int NameLength = 100;
		
		public Guid Id { get; set; }
		[StringLength(NameLength)]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}