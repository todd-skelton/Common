using Kloc.Common.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kloc.Common.Data.EntityFrameworkCore
{
    /// <summary>
    /// Repository that can only be queried and typically gets mapped to a view.
    /// </summary>
    /// <typeparam name="TEntity">The type being represented in the database.</typeparam>
    public abstract class QueryOnlyRepositoryBase<TEntity> : IQueryOnlyRepository<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// The query only dataset.
        /// </summary>
        protected readonly DbQuery<TEntity> _dbQuery;

        /// <summary>
        /// Constucts a new <see cref="QueryOnlyRepositoryBase{T}"/> from a <see cref="DbQuery{TQuery}"/>
        /// </summary>
        /// <param name="dbQuery"></param>
        public QueryOnlyRepositoryBase(DbQuery<TEntity> dbQuery)
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
