using Kloc.Common.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityFrameworkCore
{
    /// <summary>
    /// Base class for unit of work pattern using ef core. Domain events are dispatched on <see cref="CommitAsync"/>.
    /// </summary>
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDomainEventDispatcher _dispatcher;

        /// <summary>
        /// Constucts a <see cref="UnitOfWork"/> used to commit changes to a database.
        /// </summary>
        /// <param name="dbContext">The <see cref="DbContext"/> for the database.</param>
        /// <param name="dispatcher">The <see cref="IDomainEventDispatcher"/> to dispatch any events.</param>
        protected UnitOfWork(DbContext dbContext, IDomainEventDispatcher dispatcher)
        {
            _dbContext = dbContext;
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Saves changes to the <see cref="DbContext"/> and dispatches any <see cref="IDomainEvent"/> added to an <see cref="IAggregateRoot"/>.
        /// </summary>
        public virtual async Task CommitAsync()
        {
            // find and raise all domain events on entities
            var entries = _dbContext.ChangeTracker.Entries<IAggregateRoot>().Where(e => e.Entity.DomainEvents.Any());

            var aggregateRoots = entries.Select(e => e.Entity);

            var events = aggregateRoots.SelectMany(e => e.DomainEvents).ToList();

            foreach (var aggregateRoot in aggregateRoots)
            {
                aggregateRoot.ClearEvents();
            }

            // save changes to the database
            await _dbContext.SaveChangesAsync();

            if (events.Any())
            {
                await DispatchEventsAsync(_dispatcher, events);
            }
        }

        /// <summary>
        /// Dispatches each <see cref="IDomainEvent"/> that was added to any <see cref="IAggregateRoot"/> before <see cref="CommitAsync"/> was called.
        /// </summary>
        /// <param name="dispatcher">The <see cref="IDomainEventDispatcher"/>.</param>
        /// <param name="events">The <see cref="IEnumerable{T}"/> of <see cref="IDomainEvent"/>.</param>
        /// <returns></returns>
        protected virtual async Task DispatchEventsAsync(IDomainEventDispatcher dispatcher, IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                await dispatcher.RaiseAsync(@event);
            }
        }
    }
}
