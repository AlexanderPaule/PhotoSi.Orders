﻿using System;
using System.ComponentModel.DataAnnotations;
using PhotoSi.Orders.Server.Orders.Data.Models;

namespace PhotoSi.Orders.Server.Orders.Controllers.Models
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