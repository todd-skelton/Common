using System;
using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var obj = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>));

            if (obj is ICommandHandler<TCommand> handler)
                await handler.HandleAsync(command);
            else
                throw new ArgumentException($"{typeof(TCommand).FullName} does not have a valid handler of type {typeof(ICommandHandler<TCommand>).FullName} registered.");
        }
    }
}
