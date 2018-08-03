﻿using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
