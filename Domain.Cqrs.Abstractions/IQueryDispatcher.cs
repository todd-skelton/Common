using System.Threading.Tasks;

namespace Kloc.Common.Domain.Cqrs
{
    public interface IQueryDispatcher
    {
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}
