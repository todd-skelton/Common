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

        public abstract IEnumerable<IDomainEvent> GetDomainEvents();

        public virtual async Task CommitAsync()
        {
            foreach(var @event in GetDomainEvents())
            {
                await _dispatcher.RaiseAsync(@event);
            }
        }
    }
}
