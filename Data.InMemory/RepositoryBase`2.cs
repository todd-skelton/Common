using Kloc.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kloc.Common.Data.InMemory
{
    /// <summary>
    /// Base class for an in-memory repository implementation
    /// </summary>
    /// <typeparam name="TAggregateRoot">The entity. It must be a <see cref="IAggregateRoot"/>.</typeparam>
    public abstract class RepositoryBase<TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        /// <summary>
        /// The collection of entities to hold in memory.
        /// </summary>
        protected readonly HashSet<TAggregateRoot> entitySet = new HashSet<TAggregateRoot>();

        /// <summary>
        /// Adds an entity to the collection.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public virtual void Add(TAggregateRoot entity)
        {
            entitySet.Add(entity);
        }

        /// <summary>
        /// Removes an entity from the collection
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public virtual void Remove(TAggregateRoot entity)
        {
            entitySet.Remove(entity);
        }

        /// <summary>
        /// Retrieves an entity from the collection by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity.</param>
        /// <returns>The entity.</returns>
        public Task<TAggregateRoot> GetByIdAsync(TKey id)
        {
            var entity = entitySet.FirstOrDefault(e => e.Id.Equals(id));
            return Task.FromResult(entity);
        }

        /// <summary>
        /// Retrieves an entity from the collection by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity.</param>
        /// <returns>The entity.</returns>
        public Task<TAggregateRoot> GetByIdAsync(params object[] id)
        {
            var entity = entitySet.FirstOrDefault(e => e.Id.Equals(id.FirstOrDefault()));
            return Task.FromResult(entity);
        }

        /// <summary>
        /// Creates an <see cref="IQueryable{T}"/> from the collection.
        /// </summary>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public virtual IQueryable<TAggregateRoot> Query()
        {
            return entitySet.AsQueryable();
        }
    }
}
