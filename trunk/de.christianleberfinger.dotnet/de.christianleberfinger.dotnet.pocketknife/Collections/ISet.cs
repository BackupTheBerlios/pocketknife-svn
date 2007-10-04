using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.pocketknife.Collections
{
    /// <summary>
    /// A collection that contains no duplicate elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISet<T> : ICollection<T>
    {
        /// <summary>
        /// Returns an array that contains all elements of this set.
        /// </summary>
        /// <returns></returns>
        T[] ToArray();

        void AddRange(IEnumerable<T> items);
    }
}
