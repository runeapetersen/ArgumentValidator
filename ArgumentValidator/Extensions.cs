using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Linq
{
    /// <summary>
    /// The extensions.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Tests if a set contains at least one null reference.
        /// </summary>
        /// <typeparam name="T">The type contained in the set</typeparam>
        /// <param name="enumerable">The extended type reference</param>
        /// <returns>A boolean indicating the result of the test</returns>
        internal static bool AnyNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any(n => (n == null));
        }

        /// <summary>
        /// Encapsulates a way to retrieve the number of elements in an instance class conforming to IEnumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        internal static int Count(this IEnumerable enumerable)
        {
            return enumerable.Cast<object>().Count();
        }
    }
}

