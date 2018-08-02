using System;
using System.Threading.Tasks;

namespace Excepting.Abstractions
{
    /// <summary>
    /// An interface used to create a handler for exceptions that can be injected into services.
    /// </summary>
    /// <typeparam name="T">The type of service the handler is being used in.</typeparam>
    /// <typeparam name="TResult">The type of result the <see cref="HandleAsync(Exception, TArguments)"/> method returns.</typeparam>
    /// <typeparam name="TArguments">Strongly-typed arguments used in the <see cref="HandleAsync(Exception, TArguments)"/> method.</typeparam>
    public interface IExceptionHandler<T, TResult, TArguments>
    {
        /// <summary>
        /// This method is called inside of the try-catch block in the service.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that was caught.</param>
        /// <param name="args">Arguments that can be used in the method.</param>
        /// <returns></returns>
        Task<TResult> HandleAsync(Exception exception, TArguments args);
    }
}
