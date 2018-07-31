using System;
using System.Threading.Tasks;

namespace Excepting.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task HandleAsync(Exception exception, params object[] args);
    }
}
