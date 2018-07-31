using System;
using System.Threading.Tasks;

namespace Excepting.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TArguments"></typeparam>
    public interface IExceptionHandler<T, TResult, TArguments>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task<TResult> HandleAsync(Exception exception, TArguments args);
    }
}
