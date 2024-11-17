using PhotoSi.Addresses.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Addresses.Controllers.Models;

public class AddrressModel
{
	[Required]
	public Guid Id { get; set; }

	[Required]
	public Guid UserId { get; set; }

	[Required, StringLength(AddressEntity.NameLength)]
	public string City { get; set; }

	[Required, StringLength(AddressEntity.NameLength)]
	public string Country { get; set; }
}