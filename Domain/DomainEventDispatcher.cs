using Excepting.Abstractions;
using Kloc.Common.Domain.Abstractions;
using Kloc.Common.Excepting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Kloc.Common.Domain
{
    /// <summary>
    /// Standard implementation for the domain event dispatcher (<see cref="IDomainEventDispatcher"/>).
    /// </summary>
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExceptionHandler _exceptionHandler;

        /// <summary>
        /// Constucts a new domain event dispatcher (<see cref="DomainEventDispatcher"/>) using the service provider (<see cref="IServiceProvider"/>)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="exceptionHandler"></param>
        public DomainEventDispatcher(IServiceProvider serviceProvider, IExceptionHandler<DomainEventDispatcher> exceptionHandler = null)
        {
            Guard.ForNull(serviceProvider, nameof(serviceProvider));

            _serviceProvider = serviceProvider;
            _exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Gets the registered handlers for the domain event (<see cref="IDomainEvent"/>) and calls the handle method (<see cref="IDomainEventHandler{TEvent}.HandleAsync(TEvent)"/>).
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns></returns>
        public async Task RaiseAsync(IDomainEvent domainEvent)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            try
            {
                foreach (dynamic handler in _serviceProvider.GetServices(handlerType))
                {
                    await handler.HandleAsync((dynamic)domainEvent);
                }
            }
            catch(Exception ex) when (_exceptionHandler != null)
            {
                await _exceptionHandler.HandleAsync(ex);
            }
        }
    }
}
