﻿using System;
using System.Collections.Generic;

namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// A collection of objects are bound together by a root entity, otherwise known as an aggregate root. The aggregate root guarantees the consistency of changes being made within the aggregate by forbidding external objects from holding references to its members.
    /// </summary>
    /// <example>
    /// When you drive a car, you do not have to worry about moving the wheels forward, making the engine combust with spark and fuel, etc.; you are simply driving the car. In this context, the car is an aggregate of several other objects and serves as the aggregate root to all of the other systems.
    /// </example>
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// The global identifier for the aggregate.
        /// </summary>
        Guid Id { get; }

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
