using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Core;

internal interface IUsersRepository
{
	Task UpsertAsync(IEnumerable<User> users);
	Task<RequestResult<User, Guid>> GetAllUsersAsync();
	Task<bool> ExistsUserAsync(Guid userId);
}