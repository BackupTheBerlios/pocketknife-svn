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

        private int _millis = 0;
        private Queue<WaitObject<T>> _queue = new Queue<WaitObject<T>>();
        private Thread _thread = null;

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

        public interface IWaitObjectHandler { }
        private class WaitObject<U> : IWaitObjectHandler
        {
            public DateTime timeElapsed;
            public U userObject;
            public WaitObject(DateTime timeElapsed, U userObject)
            {
                this.timeElapsed = timeElapsed;
                this.userObject = userObject;
            }
        }

        /// <summary>
        /// Starts a countdown of the length that you specified in the constructor.
        /// When the countdown is elapsed, an event of type <see cref="OnCountdownElapsed"/> will
        /// be raised.
        /// </summary>
        /// <param name="userObject">Any kind of user object that you will receive with the event.</param>
        public IWaitObjectHandler startCountdown(T userObject)
        {
            if (_thread == null)
            {
                _thread = new Thread(new ThreadStart(runThread));
                _thread.Name = "Countdown thread";
                _thread.IsBackground = true;
                _thread.Start();
            }

            WaitObject<T> wo = new WaitObject<T>(DateTime.Now.AddMilliseconds(_millis), userObject);
            _queue.Enqueue(wo);
            
            resetEvent.Set();

            return wo;
        }

        //TODO
        //public void cancel(IWaitObjectHandler waitHandler)
        //{
        //    lock (queueLock)
        //    {
        //    }
        //}

        bool running = true;
        AutoResetEvent resetEvent = new AutoResetEvent(false);
        object queueLock = new object();

        void runThread()
        {
            while (running)
            {
                if (_queue.Count > 0)
                {
                    WaitObject<T> wo = _queue.Peek();

                    DateTime now = DateTime.Now;
                    TimeSpan timeTillElapsed = wo.timeElapsed - now;

                    if (timeTillElapsed.TotalMilliseconds < 0)
                    {
                        // remove element from queue
                        _queue.Dequeue();

                        // Event auslösen
                        CountdownElapsedArgs<T> args = new CountdownElapsedArgs<T>(wo.userObject);
                        EventHelper.invoke<Countdown<T>, CountdownElapsedArgs<T>>(OnCountdownElapsed, this, args);
                    }
                    else
                    {
                        // die verbliebende Zeit warten
                        Thread.Sleep(timeTillElapsed);
                    }
                }
                else
                {
                    // warten bis wieder Elemente in der Queue sind.
                    resetEvent.WaitOne();
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
            running = false;
            resetEvent.Set();
            resetEvent.Close();

            if (_thread != null)
            {
                _thread.Abort();
                _thread = null;
            }
        }

        #endregion
    }
}
