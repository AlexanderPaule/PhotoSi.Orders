namespace PhotoSi.Products.Utils.Demo.Models;

public class DemoProductModel
{
	public Guid Id { get; set; }
	public DemoCategoryModel DemoCategory { get; set; }
	public string Description { get; set; }
	public IEnumerable<DemoOptionModel> Options { get; set; }
}