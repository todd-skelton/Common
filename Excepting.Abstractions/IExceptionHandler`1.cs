namespace Excepting
{
    /// <summary>
    /// An interface used to create a handler for exceptions that can be injected into services.
    /// </summary>
    /// <remarks>
    /// Use this interface over <see cref="IExceptionHandler"/> when registering with dependency injection.
    /// </remarks>
    /// <typeparam name="T">The type of service the handler is being used in.</typeparam>
    public interface IExceptionHandler<T> : IExceptionHandler
    {

    }
}
