using System;

namespace Kloc.Common.Domain.Abstractions
{
    /// <summary>
    /// An object that is not defined by its attributes, but rather by a thread of continuity and its identity.
    /// </summary>
    /// <example>
    /// Most airlines distinguish each seat uniquely on every flight. Each seat is an entity in this context. However, Southwest Airlines, EasyJet and Ryanair do not distinguish between every seat; all seats are the same. In this context, a seat is actually a value object.
    /// </example>
    public interface IEntity
    {

    }
}
