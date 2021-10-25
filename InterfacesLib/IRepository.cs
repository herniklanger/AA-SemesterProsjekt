using System.Collections.Generic;
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
		Task<int> UpdateAsync(TEntity entity, CancellationToken token = default);
		Task<int> DeleteAsync(TKey id, CancellationToken token = default);
		Task<int> DeleteAsync(TEntity entity, CancellationToken token = default);

		/// <summary>
		/// <inheritdoc cref="Repository{TEntity,TKey}.CreateAsync"/>
		/// Or:
		/// <inheritdoc cref="Repository{TEntity,TKey}.UpdateAsync"/>
		/// If the <paramref name="entity"/> already exists.
		/// </summary>
		Task<object?> UpsertAsync(TEntity entity, CancellationToken token = default);

		/// <inheritdoc cref="Enumerable.Any{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,bool})"/>
		Task<bool> ExistsAsync(TKey id, CancellationToken token = default);
	}
}