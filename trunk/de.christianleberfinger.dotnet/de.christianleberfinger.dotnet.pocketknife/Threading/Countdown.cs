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
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife.Threading
{
    /// <summary>
    /// An easy to use countdown class.
    /// Just create an instance, subscribe the <see cref="OnCountdownElapsed"/> event and
    /// start your countdowns. You will be noticed and can specify a generic user object 
    /// that you will receive in the eventarg object when the countdown is elapsed.
    /// </summary>
    /// <typeparam name="T">Type of your user object</typeparam>
    /// <remarks>The callback event is called from an internal thread.</remarks>
    public class Countdown<T> : IDisposable
    {
        /// <summary>
        /// A handle that is used to identify a pending countdown action
        /// and keep the user data.
        /// </summary>
        public class CountdownHandle
        {
            internal readonly DateTime timeElapsed;
            T userObject = default(T);
            internal CountdownHandle(DateTime timeElapsed, T userObject)
            {
                this.timeElapsed = timeElapsed;
                this.userObject = userObject;
            }
            /// <summary>
            /// Returns the user object that is linked with the Countdown event
            /// or default(T) if none was specified.
            /// </summary>
            public T UserObject { get { return userObject; } }
        }

        /// <summary>
        /// Contains user information that is connected with a countdown elapsed event.
        /// </summary>
        public class CountdownElapsedArgs : EventArgs
        {
            private T _userObject;
            /// <summary>
            /// ctor.
            /// </summary>
            /// <param name="userobject">Any object you would like to receive with 
            /// a raised elapsed event.</param>
            public CountdownElapsedArgs(T userobject)
            {
                _userObject = userobject;
            }
            /// <summary>
            /// The userobject you defined when starting the countdown.
            /// Can be null!
            /// </summary>
            public T UserObject { get { return _userObject; } }
        }

        /// <summary>
        /// Is raised when the countdown is elapsed.
        /// </summary>
        public event GenericEventHandler<Countdown<T>, CountdownElapsedArgs> OnCountdownElapsed;

        /// <summary>
        /// The thread that is waiting for the current countdown to elapse.
        /// </summary>
        private NiceThread _waitingThread = null;

        /// <summary>
        /// Controls the thread that is waiting for the countdowns to elapse
        /// </summary>
        AutoResetEvent _waitHandle = new AutoResetEvent(false);

        /// <summary>
        /// locking variable for the waiting queue.
        /// </summary>
        object _queueLock = new object();

        /// <summary>
        /// contains all pending countdowns
        /// </summary>
        //private SortedList<DateTime, CountdownHandle> _queue = new SortedList<DateTime, CountdownHandle>();
        private LinkedList<CountdownHandle> _queue = new LinkedList<CountdownHandle>();
        //private List<CountdownHandle> _queue = new List<CountdownHandle>();

        /// <summary>
        /// Creates a new instance of the countdown class. You can start countdowns and will be
        /// informed by an event when the countdown is elapsed.
        /// </summary>
        public Countdown()
        {
        }

        /// <summary>
        /// destructor. 
        /// </summary>
        ~Countdown()
        {
            Dispose();
        }

        DateTime _nextElapsedCountdown = DateTime.Now;

        /// <summary>
        /// Starts a countdown of the specified length.
        /// When the countdown is elapsed, an event of type <see cref="OnCountdownElapsed"/> will
        /// be raised.
        /// </summary>
        /// <param name="userObject">Any kind of user object that you will receive with the event.</param>
        /// <param name="millis">The countdowns duration [ms].</param>
        public CountdownHandle startCountdown(T userObject, int millis)
        {
            if (_waitingThread == null)
            {
                _waitingThread = new NiceThread(new ThreadStart(runThread), "Countdown thread");
                _waitingThread.start();
            }

            DateTime timeElapsed = DateTime.Now.AddMilliseconds(millis);

            // store next elapsed datetime
            if (timeElapsed < _nextElapsedCountdown)
                _nextElapsedCountdown = timeElapsed;

            CountdownHandle wo = new CountdownHandle(timeElapsed, userObject);
            enqueue(wo);

            // resume the waiting thread if it's currently blocked
            _waitHandle.Set();

            return wo;
        }

        void enqueue(CountdownHandle wo)
        {
            lock (_queueLock)
            {
                _queue.AddLast(wo);
            }
        }

        /// <summary>
        /// get most urgent countdown handle from queue
        /// </summary>
        /// <returns></returns>
        CountdownHandle dequeue(DateTime now)
        {
            CountdownHandle wo = null;
            lock (_queueLock)
            {
                foreach (CountdownHandle node in _queue)
                {
                    // current CountdownHandle is elapsed
                    if (now > node.timeElapsed)
                    {
                        wo = node;
                        break;
                    }
                }

                if (wo != null)
                    _queue.Remove(wo);
            }
            return wo;
        }

        //CountdownHandle peek()
        //{
        //    CountdownHandle wo = null;
        //    lock (_queueLock)
        //    {
        //        LinkedListNode<CountdownHandle> firstNode = _queue.First;
        //        if (firstNode != null)
        //            wo = firstNode.Value;
        //    }
        //    return wo;
        //}

        /// <summary>
        /// Gets a copy of the currently pending operations.
        /// </summary>
        /// <returns>array of pending operations or null if there are 
        /// no pending operations</returns>
        public CountdownHandle[] getPendingOperations()
        {
            CountdownHandle[] retVal = null;
            lock (_queueLock)
            {
                retVal = new CountdownHandle[_queue.Count];
                _queue.CopyTo(retVal, 0);
            }
            return retVal;
        }

        /// <summary>
        /// Cancel a pending countdown operation. if the operation couldn't be found,
        /// false will be returned. 
        /// </summary>
        /// <param name="handle">The countdown operation you want to cancel.</param>
        public bool cancel(CountdownHandle handle)
        {
            bool removed = false;
            lock (_queueLock)
            {
                removed = _queue.Remove(handle);
            }

            return removed;
        }

        /// <summary>
        /// Cancels all pending countdown operations.
        /// </summary>
        public void cancelAll()
        {
            lock (_queueLock)
            {
                _queue.Clear();
                _waitHandle.Set();
            }
        }

        //bool _running = true;

        void runThread()
        {
            while (_waitingThread != null && _waitingThread.Running)
            {
                if (_queue.Count > 0)
                {
                    DateTime now = DateTime.Now;
                    TimeSpan timeTillElapsed = _nextElapsedCountdown - now;

                    if (timeTillElapsed.TotalMilliseconds < 0)
                    {
                        // remove element from queue
                        CountdownHandle wo = dequeue(now);

                        if (wo != null)
                        {
                            // Event auslösen
                            CountdownElapsedArgs args = new CountdownElapsedArgs(wo.UserObject);
                            EventHelper.invoke<Countdown<T>, CountdownElapsedArgs>(OnCountdownElapsed, this, args);
                        }
                    }
                    else
                    {
                        // die verbliebene Zeit warten
                        _waitHandle.WaitOne(timeTillElapsed, false);
                    }
                }
                else
                {
                    // warten bis wieder Elemente in der Queue sind.
                    _waitHandle.WaitOne();
                }
            }
        }

        #region IDisposable Member

        /// <summary>
        /// Release ressources
        /// </summary>
        public void Dispose()
        {
            Debug.WriteLine("Countdown wird disposed.");

            if (_waitingThread != null)
            {
                _waitingThread.Kill();
                _waitingThread = null;
            }

            if (_waitHandle != null)
            {
                _waitHandle.Close();
            }
        }

        #endregion
    }
}
