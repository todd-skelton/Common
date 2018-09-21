using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
