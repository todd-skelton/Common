using System;
using System.Threading.Tasks;

namespace Kloc.Common.Excepting
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public Task HandleAsync(Exception exception, params object[] args)
        {
            throw new Exception("Encountered exception.", exception);
        }

        public Task<TResult> HandleAsync<TResult>(Exception exception, params object[] args)
        {
            throw new Exception("Encountered exception.", exception);
        }
    }
}
