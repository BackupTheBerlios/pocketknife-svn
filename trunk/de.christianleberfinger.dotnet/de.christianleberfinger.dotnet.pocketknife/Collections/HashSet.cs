/*
 * 
 * Copyright (c) 2007 Christian Leberfinger
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN 
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.pocketknife.Collections
{
    /// <summary>
    /// An implementation if the interface ISet that is based on a dictionary.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HashSet<T> : ISet<T>
    {
        private Dictionary<T, object> _map = new Dictionary<T, object>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HashSet()
        {
        }

        /// <summary>
        /// Creates a new HashSet that contains the given items.
        /// </summary>
        /// <param name="items"></param>
        public HashSet(IEnumerable<T> items)
        {
            AddRange(items);
        }

        #region ICollection<T> Member

        /// <summary>
        /// Adds an item to this set.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if(! _map.ContainsKey(item))
                _map.Add(item, null);
        }

        /// <summary>
        /// Adds all elements of an enumeration to this set.
        /// Elements that are already contained by the set are ignored.
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                try
                {
                    Add(item);
                }
                catch { }
            }
        }

        /// <summary>
        /// Removes all items from this set.
        /// </summary>
        public void Clear()
        {
            _map.Clear();
        }

        /// <summary>
        /// Returns a boolean value that indicates whether this Set contains the given item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _map.ContainsKey(item);
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException("The method 'CopyTo' is not implemented yet.");
        }

        /// <summary>
        /// Returns number of elements in Set.
        /// </summary>
        public int Count
        {
            get { return _map.Count; }
        }

        /// <summary>
        /// Always writeable.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the given item from this set.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return _map.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Member

        /// <summary>
        /// Returns an enumerator for this set.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _map.Keys.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _map.Keys.GetEnumerator();
        }

        /// <summary>
        /// Returns a new array that contains all elements of this set.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            List<T> temp = new List<T>(_map.Keys);
            return temp.ToArray();
        }

        #endregion
    }
}
