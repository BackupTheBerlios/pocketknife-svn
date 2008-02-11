/*
 * 
 * Copyright (c) 2008 Christian Leberfinger
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
using System.Threading;

namespace de.christianleberfinger.dotnet.pocketknife.Collections
{
    /// <summary>
    /// A priority queue. When inserting an element, you define its priority.
    /// For each priority a new internal <see cref="Queue&lt;T&gt;"/> is created. 
    /// So when you want to save memory, use a small number of different priorities.
    /// You can define them for example within an enumeration.
    /// </summary>
    /// <typeparam name="TPrio">The type that defines the priority. Should of course be comparable.</typeparam>
    /// <typeparam name="TElement">The type of your queued elements.</typeparam>
    public class PriorityQueue<TPrio, TElement> : IEnumerable<TElement>
    {
        SortedList<TPrio, Queue<TElement>> _list = new SortedList<TPrio, Queue<TElement>>();
        object _lockVar = new object();
        int _count = 0;

        /// <summary>
        /// Enqueues an element with the given priority.
        /// </summary>
        /// <param name="priority">The elements priority, e.g. MyPriorities.High.</param>
        /// <param name="element">The element you want to enqueue.</param>
        public void enqueue(TPrio priority, TElement element)
        {
            Queue<TElement> q = null;
            lock (_lockVar)
            {
                bool found = _list.TryGetValue(priority, out q);
                if (!found)
                {
                    // create a new queue for the given priority
                    q = new Queue<TElement>();
                    _list.Add(priority, q);
                }

                q.Enqueue(element);
                _count++;
            }
        }

        /// <summary>
        /// Returns the number of elements in the queue. O(1)
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Returns the 'oldest' element with the highest priority and removes it from the queue.
        /// If the queue is empty, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public TElement dequeue()
        {
            lock (_lockVar)
            {
                foreach (KeyValuePair<TPrio, Queue<TElement>> kvPair in _list)
                {
                    Queue<TElement> q = kvPair.Value;

                    if (q != null && q.Count > 0)
                    {
                        _count--;
                        return q.Dequeue();
                    }
                }

                throw new InvalidOperationException("The Queue is empty.");
            }
        }

        /// <summary>
        /// Returns the 'oldest' element with the highest priority WITHOUT removing it from the queue. 
        /// If the queue is empty, default(TElement) is being returned.
        /// </summary>
        /// <returns></returns>
        public TElement peek()
        {
            lock (_lockVar)
            {
                foreach (KeyValuePair<TPrio, Queue<TElement>> kvPair in _list)
                {
                    Queue<TElement> q = kvPair.Value;

                    if (q != null && q.Count > 0)
                    {
                        _count--;
                        return q.Peek();
                    }
                }

                throw new InvalidOperationException("The Queue is empty.");
            }
        }

        /// <summary>
        /// Clears all queued entries.
        /// </summary>
        public void clear()
        {
            lock (_lockVar)
            {
                _list.Clear();
                _count = 0;
            }
        }

        #region IEnumerable<TElement> Member

        /// <summary>
        /// Returns an enumerator that traverses through the elements of the priority queue.
        /// Elements are sorted due to their priority. Objects with same priority are returned in FIFO order.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<TElement> GetEnumerator()
        {
            foreach (KeyValuePair<TPrio, Queue<TElement>> kvPair in _list)
            {
                Queue<TElement> q = kvPair.Value;

                foreach(TElement e in q)
                {
                    yield return e;
                }
            }
        }

        #endregion

        #region IEnumerable Member

        /// <summary>
        /// Returns an enumerator that traverses through the elements of the priority queue.
        /// Elements are sorted due to their priority. Objects with same priority are returned in FIFO order.
        /// </summary>
        /// <returns>Enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
