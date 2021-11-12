using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterfacesLib;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace DatabaseLayerCore
{
    public abstract class Repository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected readonly IDbConnection Connection;

        protected Repository(IDbConnectionFactory connection)
        {
            Connection = connection.OpenDbConnection();
        }

        public virtual async Task<int> CreateAsync(TEntity entity, CancellationToken token = default)
        {
            await Connection.SaveAsync(entity, true, token: token);
            return entity.Id;
        }

        public virtual async Task<TEntity?> GetAsync(int id, CancellationToken token = default)
        {
            TEntity result = (await Connection.LoadSingleByIdAsync<TEntity>(id, token: token));
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
        {
            return await Connection.SelectAsync<TEntity>(token: token);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken token = default)
        {
            await Connection.SaveAsync(entity, true, token: token);
            return entity.Id;
        }

        public virtual async Task<int> DeleteAsync(int id, CancellationToken token = default)
        {
            var result =await Connection.DeleteByIdAsync<TEntity>(id, token: token);
            return result;
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
        public async Task<int> UpsertAsync(TEntity entity, CancellationToken token = default)
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