using Kloc.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kloc.Common.Data.InMemory
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        protected readonly IDomainEventDispatcher _dispatcher;

        protected UnitOfWorkBase(IDomainEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public abstract IEnumerable<IAggregateRoot> GetEntities();

        public virtual async Task CommitAsync()
        {
            var events = new List<IDomainEvent>();

            foreach(var entity in GetEntities().Where(e => e.DomainEvents.Any()))
            {
                events.AddRange(entity.DomainEvents);
                entity.ClearEvents();
            }

            foreach (var @event in events)
            {
                await _dispatcher.RaiseAsync(@event);
            }
        }
    }
}
