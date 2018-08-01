using System;
using System.Threading.Tasks;

namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// Standard repository interface with crud operations.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of aggregate root (<see cref="IAggregateRoot"/>) the repository is responsible for.</typeparam>
    public interface IRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        /// <summary>
        /// Gets the entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity.</param>
        /// <returns>The entity.</returns>
        Task<TAggregateRoot> GetByIdAsync(Guid id);
        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TAggregateRoot entity);
        /// <summary>
        /// Removes an aggregate root from the repository.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TAggregateRoot entity);
    }
}
