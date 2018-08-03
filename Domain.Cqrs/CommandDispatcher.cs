using Kloc.Common.Excepting;
using System;
using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExceptionHandler _exceptionHandler;

        public CommandDispatcher(IServiceProvider serviceProvider, IExceptionHandler<CommandDispatcher> exceptionHandler = null)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _exceptionHandler = exceptionHandler;
            _exceptionHandler = _exceptionHandler ?? new DefaultExceptionHandler();
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) ?? 
                throw new ArgumentException($"{typeof(TCommand).FullName} does not have a valid handler of type {typeof(ICommandHandler<TCommand>).FullName} registered.");

            try
            {
                await handler.HandleAsync(command);
            }
            catch(Exception ex)
            {
                await _exceptionHandler.HandleAsync(ex);
            }
        }
    }
}
