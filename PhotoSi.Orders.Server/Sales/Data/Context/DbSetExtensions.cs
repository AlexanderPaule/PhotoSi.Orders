using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhotoSi.Orders.Server.Sales.Data.Context
{
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
}