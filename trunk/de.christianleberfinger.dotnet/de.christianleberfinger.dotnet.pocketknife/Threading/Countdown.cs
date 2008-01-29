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
    /// A easy to use countdown class.
    /// Just create an instance, subscribe the <see cref="OnCountdownElapsed"/> event and
    /// start your countdowns. You will be noticed and can specify a generic user object 
    /// that you will receive in the eventarg object when the countdown is elapsed.
    /// </summary>
    /// <typeparam name="T">Type of your user object</typeparam>
    /// <remarks>The callback event is called from an internal thread.</remarks>
    public class Countdown<T> : IDisposable
    {
        /// <summary>
        /// Contains user information that is connected with a countdown elapsed event.
        /// </summary>
        /// <typeparam name="U">The type of your userobject.</typeparam>
        public class CountdownElapsedArgs<U> : EventArgs
        {
            private U _userObject;
            /// <summary>
            /// ctor.
            /// </summary>
            /// <param name="userobject">Any object you would like to receive with 
            /// a raised elapsed event.</param>
            public CountdownElapsedArgs(U userobject)
            {
                _userObject = userobject;
            }
            /// <summary>
            /// The userobject you defined when starting the countdown.
            /// Can be null!
            /// </summary>
            public U UserObject { get { return _userObject; } }
        }

        /// <summary>
        /// Is raised when the countdown is elapsed.
        /// </summary>
        public event GenericEventHandler<Countdown<T>, CountdownElapsedArgs<T>> OnCountdownElapsed;

        /// <summary>
        /// Defines the duration of each countdown in milliseconds.
        /// </summary>
        private int _millis = 0;

        /// <summary>
        /// The thread that is waiting for the current countdown to elapse.
        /// </summary>
        private Thread _waitingThread = null;

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
        private LinkedList<CountdownHandle<T>> _queue = new LinkedList<CountdownHandle<T>>();

        /// <summary>
        /// Creates a new instance of the countdown class. You can start countdowns and will be
        /// informed by an event when the countdown is elapsed.
        /// </summary>
        /// <param name="milliSeconds">The length of the countdown.</param>
        public Countdown(int milliSeconds)
        {
            _millis = milliSeconds;
        }

        /// <summary>
        /// destructor. 
        /// </summary>
        ~Countdown()
        {
            Dispose();
        }

        /// <summary>
        /// A handle that you can use to cancel a pending countdown action
        /// </summary>
        /// <typeparam name="U"></typeparam>
        public class CountdownHandle<U>
        {
            internal readonly DateTime timeElapsed;
            U userObject = default(U);
            internal CountdownHandle(DateTime timeElapsed, U userObject)
            {
                this.timeElapsed = timeElapsed;
                this.userObject = userObject;
            }
            /// <summary>
            /// Returns the user object that is linked with the Countdown event
            /// or default(U) if none was specified.
            /// </summary>
            public U UserObject { get { return userObject; } }
        }

        /// <summary>
        /// Starts a countdown of the length that you specified in the constructor.
        /// When the countdown is elapsed, an event of type <see cref="OnCountdownElapsed"/> will
        /// be raised.
        /// </summary>
        /// <param name="userObject">Any kind of user object that you will receive with the event.</param>
        public CountdownHandle<T> startCountdown(T userObject)
        {
            if (_waitingThread == null)
            {
                _waitingThread = new Thread(new ThreadStart(runThread));
                _waitingThread.Name = "Countdown thread";
                _waitingThread.IsBackground = true;
                _waitingThread.Start();
            }

            CountdownHandle<T> wo = new CountdownHandle<T>(DateTime.Now.AddMilliseconds(_millis), userObject);
            enqueue(wo);
            
            _waitHandle.Set();

            return wo;
        }

        void enqueue(CountdownHandle<T> wo)
        {
            lock (_queueLock)
            {
                _queue.AddLast(wo);
            }
        }

        CountdownHandle<T> dequeue()
        {
            CountdownHandle<T> wo;
            lock (_queueLock)
            {
                wo = _queue.First.Value;
                _queue.RemoveFirst();
            }
            return wo;
        }

        CountdownHandle<T> peek()
        {
            lock (_queueLock)
            {
                return _queue.First.Value;
            }
        }

        /// <summary>
        /// Cancel a pending countdown operation. if the operation couldn't be found,
        /// false will be returned. 
        /// </summary>
        /// <param name="handle">The countdown operation you want to cancel.</param>
        public bool cancel(CountdownHandle<T> handle)
        {
            lock (_queueLock)
            {
                // find the node that has to be removed
                LinkedListNode<CountdownHandle<T>> node = _queue.Find(handle);

                // node wasn't found
                if (node == null)
                {
                    return false;
                }

                // waitobject entfernen
                _queue.Remove(node);
                
                // resume waiting thread
                _waitHandle.Set();
            }

            return true;
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

        bool _running = true;

        void runThread()
        {
            while (_running)
            {
                if (_queue.Count > 0)
                {
                    CountdownHandle<T> wo = peek();

                    DateTime now = DateTime.Now;
                    TimeSpan timeTillElapsed = wo.timeElapsed - now;

                    if (timeTillElapsed.TotalMilliseconds < 0)
                    {
                        // remove element from queue
                        dequeue();

                        // Event auslösen
                        CountdownElapsedArgs<T> args = new CountdownElapsedArgs<T>(wo.UserObject);
                        EventHelper.invoke<Countdown<T>, CountdownElapsedArgs<T>>(OnCountdownElapsed, this, args);
                    }
                    else
                    {
                        // die verbliebende Zeit warten
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
        /// 
        /// </summary>
        public void Dispose()
        {
            Debug.WriteLine("Countdown wird disposed.");
            _running = false;
            _waitHandle.Set();
            _waitHandle.Close();

            if (_waitingThread != null)
            {
                _waitingThread.Abort();
                _waitingThread = null;
            }
        }

        #endregion
    }
}
