using Kloc.Common.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.EntityFrameworkCore
{
    /// <summary>
    /// Repository that can only be queried and typically gets mapped to a view.
    /// </summary>
    /// <typeparam name="TEntity">The type being represented in the database.</typeparam>
    public abstract class QueryOnlyRepository<TEntity> : IQueryOnlyRepository<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// The query only dataset.
        /// </summary>
        protected readonly DbQuery<TEntity> _dbQuery;

        /// <summary>
        /// Constucts a new <see cref="QueryOnlyRepository{T}"/> from a <see cref="DbQuery{TQuery}"/>
        /// </summary>
        /// <param name="dbQuery"></param>
        public QueryOnlyRepository(DbQuery<TEntity> dbQuery)
        {
            _dbQuery = dbQuery;
        }

        /// <summary>
        /// Gets the <see cref="DbQuery{TQuery}"/> as an <see cref="IQueryable{T}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{T}"/></returns>
        public abstract IQueryable<TEntity> Query();
    }
}
