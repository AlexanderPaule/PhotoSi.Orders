namespace PhotoSi.Users.Core.Models;

public class RequestResult<TObj, TId> where TObj : class
{
	private readonly IEnumerable<TObj> _foundObjects;
	private readonly IEnumerable<TId> _searchedIds;

	private RequestResult(IEnumerable<TObj> foundObject, IEnumerable<TId> searchedIds)
	{
		_foundObjects = foundObject;
		_searchedIds = searchedIds;
	}

	public TObj GetScalar()
	{
		return _foundObjects.Single();
	}

	public IReadOnlyCollection<TObj> GetList()
	{
		return _foundObjects.ToList();
	}

	public bool FoundAll()
	{
		return _searchedIds.Count() == _foundObjects.Count();
	}

	public static RequestResult<TObj, TId> New(IEnumerable<TObj> requestedObjects, IEnumerable<TId> searchedIds)
	{
		return new RequestResult<TObj, TId>(requestedObjects.Where(x => x != null).ToList(), searchedIds);
	}

	public static RequestResult<TObj, TId> NewNotFound(IEnumerable<TId> searchedIds)
	{
		return new RequestResult<TObj, TId>(Enumerable.Empty<TObj>(), searchedIds);
	}
}