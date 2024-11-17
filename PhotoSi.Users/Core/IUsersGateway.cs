using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Core;

public interface IUsersGateway
{
	Task UpsertAsync(IEnumerable<User> categories);
	Task<RequestResult<User, Guid>> GetAllUsersAsync();
}