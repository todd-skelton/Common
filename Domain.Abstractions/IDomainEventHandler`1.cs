using System.Threading.Tasks;

namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// Interface to define a domain event handler to handle events (<see cref="IDomainEvent"/>).
    /// </summary>
    /// <typeparam name="TEvent">The <see cref="IDomainEvent"/> type to be handled</typeparam>
    public interface IDomainEventHandler<TEvent> 
        where TEvent : IDomainEvent
    {
        /// <summary>
        /// Processes the <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="event">The <see cref="IDomainEvent"/> to process.</param>
        Task HandleAsync(TEvent @event);
    }
}
