using System;
using PhotoSi.Orders.Server.Orders.Controllers.Models;

namespace PhotoSi.Orders.Server.Orders.Core.Dto
{
	internal class Product
	{
		public Guid Id { get; set; }
		public CategoryModel Category { get; set; }
	}
}