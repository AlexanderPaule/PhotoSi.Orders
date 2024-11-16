using System;
using System.ComponentModel.DataAnnotations;
using PhotoSi.Sales.Sales.Data.Models;

namespace PhotoSi.Sales.Orders.Models;

public class OrderedOptionModel
{
	[Required]
	public Guid Id { get; set; }
	[Required, StringLength(OptionEntity.NameLength)]
	public string Name { get; set; }
	[Required]
	public string Content { get; set; }
}