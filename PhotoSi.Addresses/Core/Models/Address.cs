namespace PhotoSi.Addresses.Core.Models;

public class Address
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
}