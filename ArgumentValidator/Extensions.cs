/*
 * Copyright 2012 Rune Allan Petersen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;

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

