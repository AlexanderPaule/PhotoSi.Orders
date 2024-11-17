using PhotoSi.Users.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Users.Controllers.Models;

public class UserModel
{
	[Required]
	public Guid Id { get; set; }
	[Required, StringLength(UserEntity.NameLength)]
	public string Name { get; set; }
	[Required, StringLength(UserEntity.NameLength)]
	public string Surname { get; set; }
}