using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Users.Data.Context;

internal static class DbSetExtensions
{
	public static async Task UpsertBulkAsync<TEntity>(
		this DbSet<TEntity> source,
		IEnumerable<TEntity> entities,
		Expression<Func<TEntity, bool>> dbFilter) where TEntity : class
	{
		var toRemoveEntities = await source
			.Where(dbFilter)
			.ToListAsync();

		if (toRemoveEntities.Any())
			source.RemoveRange(toRemoveEntities);

		await source
			.AddRangeAsync(entities);
	}
}