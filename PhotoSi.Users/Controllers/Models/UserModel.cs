using System.ComponentModel.DataAnnotations;

namespace PhotoSi.Users.Controllers.Models;

public class UserModel
{
	[Required]
	public Guid Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string Surname { get; set; }
}