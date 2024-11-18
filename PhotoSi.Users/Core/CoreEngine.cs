using PhotoSi.Users.Core.Models;

namespace PhotoSi.Users.Core;

public class CoreEngine : IUsersGateway
{
	private readonly IUsersRepository _repository;

	public CoreEngine(IUsersRepository repository)
	{
		_repository = repository;
	}

	public Task UpsertAsync(IEnumerable<User> users)
	{
		return _repository
			.UpsertAsync(users);
	}

	public Task<RequestResult<User, Guid>> GetAllUsersAsync()
	{
		return _repository
			.GetAllUsersAsync();
	}

	public Task<bool> ExistsUserAsync(Guid userId)
	{
		return _repository
			.ExistsUserAsync(userId);
	}
}