using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Orders.Server.Orders.Data.Models
{
	public class OrderedProductEntity
	{
		public Guid Id { get; set; }
		public IEnumerable<OptionEntity> Options { get; set; }
	}
}