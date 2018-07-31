namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// Interface used to implement the unit of work pattern
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Persists changes made in the domain.
        /// </summary>
        void Commit();
    }
}
