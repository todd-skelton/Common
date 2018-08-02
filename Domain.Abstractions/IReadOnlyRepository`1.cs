using System.Threading.Tasks;

namespace Kloc.Common.Domain
{
    /// <summary>
    /// Interface that defines a repository that is read-only.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity (<see cref="IEntity"/>) the repository is responsible for.</typeparam>
    public interface IReadOnlyRepository<TEntity> : IQueryOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Gets the entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key or composite key of the entity.</param>
        /// <returns>The entity.</returns>
        Task<TEntity> GetByIdAsync(params object[] id);
    }
}
