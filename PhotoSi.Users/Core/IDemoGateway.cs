using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Core;

public interface IDemoGateway
{
	Task UpsertAsync(IEnumerable<User> users);
}