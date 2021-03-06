﻿using System;
using System.ComponentModel.DataAnnotations;
using PhotoSi.Sales.Sales.Data.Models;

namespace PhotoSi.Sales.Orders.Models
{
	public class OrderCategoryModel
	{
		[Required]
		public Guid Id { get; set; }
		[StringLength(CategoryEntity.NameLength)]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}