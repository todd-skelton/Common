using System.Collections.Generic;

namespace Kloc.Common.Domain
{
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// A read-only collection (<see cref="IReadOnlyCollection{T}"/>) of domain events (<see cref="IDomainEvent"/>).
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Removes all domain events (<see cref="IDomainEvent"/>) from the collection (<see cref="DomainEvents"/>).
        /// </summary>
        void ClearEvents();
    }
}
