using System.Linq;

namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// Interface that defines a repository that is query only.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity (<see cref="IEntity"/>) the repository is responsible for.</typeparam>
    public interface IQueryOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Gets the collection of entities from the repository as a queryable collection (<see cref="IQueryable{T}"/>).
        /// </summary>
        /// <returns>The queryable collection (<see cref="IQueryable{T}"/>).</returns>
        IQueryable<TEntity> Query();
    }
}
