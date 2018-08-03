using System;
using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var obj = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

            if (obj is IQueryHandler<TQuery, TResult> handler)
            {
                return await handler.HandleAsync(query);
            }
            else
            {
                throw new ArgumentException($"{typeof(TQuery).FullName} does not have a valid handler of type {typeof(IQueryHandler<TQuery, TResult>).FullName} registered.");
            }
        }
    }
}
