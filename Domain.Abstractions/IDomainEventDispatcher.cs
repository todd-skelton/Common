using System.Threading.Tasks;

namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// Interface to define a domain event dispatcher
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches the <see cref="IDomainEvent"/> to its handlers(<see cref="IDomainEventHandler{T}"/>).
        /// </summary>
        /// <param name="domainEvent"></param>
        Task RaiseAsync(IDomainEvent domainEvent);
    }
}
