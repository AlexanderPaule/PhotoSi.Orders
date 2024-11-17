namespace PhotoSi.Products.Core.Models;

public class Product
{
	public Guid Id { get; set; }
	public Category Category { get; set; }
	public string Description { get; set; }
}