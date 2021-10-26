using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterfacesLib;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;

namespace Fleet.DataBaseLayre
{
	public abstract class Repository<TEntity> : IRepository<TEntity, int>
		where TEntity : class, IEntity<int>
	{
		protected readonly IDbConnection Connection;

		protected Repository(IDbConnectionFactory connection)
		{
			Connection = connection.OpenDbConnection();
			Connection.CreateTable<TEntity>();
		}

		public virtual async Task<int> CreateAsync(TEntity entity, CancellationToken token = default)
		{
			var id = await Connection.InsertAsync(entity, token: token);
			return (int)id;//await Connection.SaveAsync(entity, true, token: token);
		}

		public virtual async Task<TEntity?> GetAsync(int id, CancellationToken token = default)
		{
			return (await Connection.SelectAsync<TEntity>(entity => entity.Id.Equals(id), token: token)).FirstOrDefault();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
		{
			return await Connection.SelectAsync<TEntity>(token: token);
		}

		public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken token = default)
		{
			var id = await Connection.UpdateAsync(entity, token: token);
			return id;//await Connection.SaveAsync(entity, true, token: token);
		}

		public virtual async Task<int> DeleteAsync(int id, CancellationToken token = default)
		{
			return await Connection.DeleteAsync<TEntity>(id, token: token);
		}

		public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken token = default)
		{
			return await Connection.DeleteAsync(entity, token: token);
		}

		/// <summary>
		/// <inheritdoc cref="CreateAsync"/>
		/// Or:
		/// <inheritdoc cref="UpdateAsync"/>
		/// If the <paramref name="entity"/> already exists.
		/// </summary>
		public async Task<object?> UpsertAsync(TEntity entity, CancellationToken token = default)
		{
			return await ExistsAsync(entity.Id, token)
				? await UpdateAsync(entity, token)
				: await CreateAsync(entity, token);
		}

		/// <inheritdoc cref="Enumerable.Any{T}(IEnumerable{T}, Func{T, bool})"/>
		public async Task<bool> ExistsAsync(int id, CancellationToken token = default)
		{
			return (await GetAllAsync(token)).Any(entity => entity.Id.Equals(id));
		}
	}
}