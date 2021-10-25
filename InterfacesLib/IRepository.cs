﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterfacesLib
{
	public interface IRepository<TEntity, in TKey>
		where TEntity : class, IEntity<TKey>
		where TKey : notnull
	{
		Task<object?> CreateAsync(TEntity entity, CancellationToken token = default);
		Task<TEntity?> GetAsync(TKey id, CancellationToken token = default);
		Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default);
		Task<object?> UpdateAsync(TEntity entity, CancellationToken token = default);
		Task<int> DeleteAsync(TKey id, CancellationToken token = default);
		Task<int> DeleteAsync(TEntity entity, CancellationToken token = default);

		Task<object?> UpsertAsync(TEntity entity, CancellationToken token = default);

		Task<bool> ExistsAsync(TKey id, CancellationToken token = default);
	}
}