using System.Collections.Generic;

namespace Kloc.Common.Domain
{
    /// <summary>
    /// Base class for an <see cref="IAggregateRoot"/> implementation.
    /// </summary>
    public abstract class AggregateRootBase<TKey> : IAggregateRoot<TKey>
    {
        private readonly HashSet<IDomainEvent> domainEvents = new HashSet<IDomainEvent>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected AggregateRootBase(TKey id)
        {
            Id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; private set; }

        /// <summary>
        /// A <see cref="IReadOnlyCollection{T}"/> of <see cref="IDomainEvent"/> that are dispatched on a <see cref="IUnitOfWork.CommitAsync"/>.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents;

        /// <summary>
        /// Adds a <see cref="IDomainEvent"/> to the <see cref="DomainEvents"/>.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected virtual void AddDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clears the <see cref="DomainEvents"/>.
        /// </summary>
        public virtual void ClearEvents()
        {
            domainEvents.Clear();
            domainEvents.TrimExcess();
        }

        /// <summary>
        /// Compares this entity to another.
        /// </summary>
        /// <param name="obj">The entity to compare.</param>
        /// <returns>True if both references or keys are equal.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as AggregateRootBase<TKey>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Compares two entities.
        /// </summary>
        /// <param name="a">The first entity to compare.</param>
        /// <param name="b">The second entity to compare.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(AggregateRootBase<TKey> a, AggregateRootBase<TKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Compares two entities.
        /// </summary>
        /// <param name="a">The first entity to compare.</param>
        /// <param name="b">The second entity to compare.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(AggregateRootBase<TKey> a, AggregateRootBase<TKey> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Gets the hash code for the entity.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return (GetType().ToString() + Id.ToString()).GetHashCode();
        }
    }
}
