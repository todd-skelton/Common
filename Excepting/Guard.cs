using Excepting.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Kloc.Common.Excepting
{
    /// <summary>
    /// Helpers used to throw <see cref="ArgumentException"/> for certain conditions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForNullOrWhitespace(string value, string paramName, string message = "Value cannot be null, empty, or whitespace.")
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForNullOrEmpty(string value, string paramName, string message = "Value cannot be null or empty.")
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForNullOrEmpty(Guid? value, string paramName, string message = "Value cannot be null or empty.")
        {
            if (!value.HasValue || value == Guid.Empty)
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStruct"></typeparam>
        /// <param name="struct"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForDefault<TStruct>(TStruct @struct, string paramName, string message = "The value cannot be default.") 
            where TStruct : struct
        {
            if (EqualityComparer<TStruct>.Default.Equals(@struct))
                throw new ArgumentException(message, paramName);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForEmpty(Guid value, string paramName, string message = "Value cannot be empty.")
        {
            if (value == Guid.Empty)
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForNull(object value, string paramName, string message = null)
        {
            if (value is null)
                throw message is null ? new ArgumentNullException(paramName) : new ArgumentNullException(paramName, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="message"></param>
        public static void ForEquality(object value1, object value2, string message)
        {
            if (value1.Equals(value2))
                throw new ArgumentException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="message"></param>
        public static void ForInequality(object value1, object value2, string message)
        {
            if (!value1.Equals(value2))
                throw new ArgumentException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        public static void ForCondition(bool condition, string message, string paramName = null)
        {
            if (condition)
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForMaxLength(string value, int maxLength, string paramName, string message = null)
        {
            if (value?.Length > maxLength)
                throw message is null ? new ArgumentException($"Value cannot exceed {maxLength} characters.", paramName) : new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForInvalidDateFormat(string value, string paramName, string message = "Value is not a valid date.")
        {
            if (!DateTime.TryParse(value, out var result))
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ForInvalidDateFormat(string value, string format, string paramName, string message = "Value is not a valid date.")
        {
            if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                throw new ArgumentException(message, paramName);
        }
    }
}
