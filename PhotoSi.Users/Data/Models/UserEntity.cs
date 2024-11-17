using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Users.Data.Models;

public class UserEntity : TimeTrackedEntity
{
	public const int NameLength = 100;

	[Key]
	public Guid Id { get; set; }
	[Required, StringLength(NameLength)]
	public string Name { get; set; }
	[Required, StringLength(NameLength)]
	public string Surname { get; set; }
}