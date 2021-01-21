namespace PhotoSi.Orders.Server.Orders.Core
{
	public class RequestResult<T> where T : class
	{
		private RequestResult(T requestedObject)
		{
			Object = requestedObject;
		}

		public T Object { get; }

		public bool Found()
		{
			return Object != default;
		}

		public static RequestResult<T> New(T requestedObject)
		{
			return new RequestResult<T>(requestedObject);
		}

		public static RequestResult<T> NewNotFound()
		{
			return new RequestResult<T>(default);
		}
	}
}