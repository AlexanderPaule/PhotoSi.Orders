using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoSi.Orders.Server.Services.Extensions
{
	public static class EnumerableExtensions
	{
		public static string JoinStrings<T>(this IEnumerable<T> source, string separator = ",")
		{
			return string.Join(separator, source);
		}
		
		public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> source)
		{
			var enumerable = await source;
			return enumerable.ToList();
		}
	}
}
