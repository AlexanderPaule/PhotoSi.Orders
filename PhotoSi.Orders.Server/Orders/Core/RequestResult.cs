﻿using System.Collections.Generic;
using System.Linq;

namespace PhotoSi.Orders.Server.Orders.Core
{
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

		public static RequestResult<TObj, TId> New(IEnumerable<TObj> requestedObject, IEnumerable<TId> searchedIds)
		{
			return new RequestResult<TObj, TId>(requestedObject, searchedIds);
		}

		public static RequestResult<TObj, TId> NewNotFound(IEnumerable<TId> searchedIds)
		{
			return new RequestResult<TObj, TId>(Enumerable.Empty<TObj>(), searchedIds);
		}
	}
}