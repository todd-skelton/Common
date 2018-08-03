using Kloc.Common.Excepting;
using System;
using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExceptionHandler _exceptionHandler;

        public QueryDispatcher(IServiceProvider serviceProvider, IExceptionHandler<QueryDispatcher> exceptionHandler = null)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _exceptionHandler = exceptionHandler;
            _exceptionHandler = _exceptionHandler ?? new DefaultExceptionHandler();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = (IQueryHandler<TQuery,TResult>)_serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) ??
                throw new ArgumentException($"{typeof(TQuery).FullName} does not have a valid handler of type {typeof(IQueryHandler<TQuery, TResult>).FullName} registered.");

            try
            {
                return await handler.HandleAsync(query);
            }
            catch (Exception ex)
            {
                return await _exceptionHandler.HandleAsync<TResult>(ex);
            }
        }
    }
}
