using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Users.Data.Models;

internal class UserEntity : TimeTrackedEntity
{
	public const int NameLength = 100;

	public Guid Id { get; set; }
	[Required, StringLength(NameLength)]
	public string Name { get; set; }
	[Required, StringLength(NameLength)]
	public string Surname { get; set; }
}