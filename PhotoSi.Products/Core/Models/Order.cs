namespace PhotoSi.Products.Core.Models;

public class Order
{
	public Guid Id { get; set; }
	public Category Category { get; set; }
	public DateTimeOffset CreatedOn { get; set; }
	public IEnumerable<Product> Products { get; set; }
}