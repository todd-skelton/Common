using Kloc.Common.Domain;
using System;

namespace Kloc.Common.Data.InMemory
{
    public abstract class RepositoryBase<TAggregateRoot> : RepositoryBase<TAggregateRoot, Guid>, IRepository<TAggregateRoot, Guid>
        where TAggregateRoot : IAggregateRoot<Guid>
    {

    }
}
