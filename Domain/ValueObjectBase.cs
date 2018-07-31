using System.Collections.Generic;
using System.Linq;

namespace Kloc.Common.Domain
{
    /// <summary>
    /// An object that contains attributes but has no conceptual identity. They should be treated as immutable.
    /// </summary>
    /// <example>
    /// When people exchange business cards, they generally do not distinguish between each unique card; they only are concerned about the information printed on the card. In this context, business cards are value objects.
    /// </example>
    public abstract class ValueObjectBase
    {
        /// <summary>
        /// Gets the values of the object used for comparison.
        /// </summary>
        /// <example>
        /// <code>
        /// protected override IEnumerable{object} GetAtomicValues()
        /// {
        ///     yield return Value1;
        ///     yield return Value2;
        ///     yield return Value3;
        /// }
        /// </code>
        /// </example>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetAtomicValues();

        /// <summary>
        /// Compares this value object to another.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObjectBase other = (ValueObjectBase)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
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
        /// Gets the hash code for the value object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// The equality operator to compare two value objects.
        /// </summary>
        /// <param name="a">The first value object.</param>
        /// <param name="b">The second value object.</param>
        /// <returns>Returns true if both are null or the result of <see cref="Equals(object)"/>.</returns>
        public static bool operator ==(ValueObjectBase a, ValueObjectBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// The inequality operator to compare two value objects.
        /// </summary>
        /// <param name="a">The first value object.</param>
        /// <param name="b">The second value object.</param>
        /// <returns>Returns false if both objects are null or the inverse of <see cref="Equals(object)"/>.</returns>
        public static bool operator !=(ValueObjectBase a, ValueObjectBase b)
        {
            return !(a == b);
        }
    }
}
