using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Addresses.Data.Models;

public class AddressEntity : TimeTrackedEntity
{
	public const int NameLength = 100;

	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	[Required, StringLength(NameLength)]
	public string City { get; set; }
	[Required, StringLength(NameLength)]
	public string Country { get; set; }
}