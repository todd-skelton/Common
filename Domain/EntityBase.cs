using Kloc.Common.Domain.Abstractions;
using System.Collections.Generic;

namespace Kloc.Common.Domain
{

    /// <summary>
    /// Base class for an <see cref="IEntity"/> implementation.
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        /// <summary>
        /// Gets the values of the objects used to identify the entity.
        /// </summary>
        /// <example>
        /// <code>
        /// protected override IEnumerable{object} GetIdentity()
        /// {
        ///     yield return Id;
        ///     yield return Id2;
        /// }
        /// </code>
        /// </example>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetIdentity();

        /// <summary>
        /// Compares this entity to another.
        /// </summary>
        /// <param name="obj">The entity to compare.</param>
        /// <returns>True if both references or keys are equal.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as EntityBase;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            IEnumerator<object> thisValues = GetIdentity().GetEnumerator();
            IEnumerator<object> otherValues = other.GetIdentity().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <summary>
        /// Compares two entities.
        /// </summary>
        /// <param name="a">The first entity to compare.</param>
        /// <param name="b">The second entity to compare.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Compares two entities.
        /// </summary>
        /// <param name="a">The first entity to compare.</param>
        /// <param name="b">The second entity to compare.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Gets the hash code for the entity.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            string hashCode = GetType().ToString();

            foreach (var val in GetIdentity())
            {
                hashCode += " " + val.GetHashCode();
            }

            return hashCode.GetHashCode();
        }
    }
}
