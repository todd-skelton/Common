using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public interface ICommandDispatcher
    {
        Task Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
