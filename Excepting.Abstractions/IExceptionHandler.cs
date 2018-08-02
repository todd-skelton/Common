using System;
using System.Threading.Tasks;

namespace Excepting
{
    /// <summary>
    /// An interface used to create a handler for exceptions that can be injected into services.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// This method is called inside of the try-catch block in the service.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that was caught.</param>
        /// <param name="args">Arguments that can be used in the method.</param>
        /// <returns></returns>
        Task HandleAsync(Exception exception, params object[] args);
    }
}
