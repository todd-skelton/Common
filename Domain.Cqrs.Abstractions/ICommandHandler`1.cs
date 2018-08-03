using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
