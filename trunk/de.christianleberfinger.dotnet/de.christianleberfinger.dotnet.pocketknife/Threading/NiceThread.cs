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
    /// This class was heavily inspired by "Programming .NET Components" by Juval Löwy.
    /// </summary>
    public class NiceThread : IDisposable
    {
        ManualResetEvent _threadHandle;
        Thread _thread;
        bool _running;
        Mutex _runningMutex;

        /// <summary>
        /// Returns the thread's hashcode.
        /// </summary>
        /// <returns>hashcode.</returns>
        public override int GetHashCode()
        {
            return _thread.GetHashCode();
        }

        /// <summary>
        /// Compares the equality of the current and the given object.
        /// </summary>
        /// <param name="obj">o</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            return _thread.Equals(obj);
        }

        /// <summary>
        /// Gets the thread's managed ID.
        /// </summary>
        public int ThreadID { get { return _thread.ManagedThreadId; } }

        /// <summary>
        /// If you need to do direct thread manipulation (not preferred)
        /// </summary>
        public Thread Thread { get { return _thread; } }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this thread is running.
        /// </summary>
        public bool Running
        {
            get
            {
                bool r = false;
                _runningMutex.WaitOne();
                r = _running;
                _runningMutex.ReleaseMutex();
                return r;
            }
            set
            {
                _runningMutex.WaitOne();
                _running = value;
                _runningMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// destructor.
        /// </summary>
        ~NiceThread()
        {
            Kill();
        }

        /// <summary>
        /// ctor.
        /// </summary>
        public NiceThread(ThreadStart threadMethod, string threadName)
        {
            _running = true;
            _thread = null;
            _runningMutex = new Mutex();
            _threadHandle = new ManualResetEvent(false);
            _thread = new Thread(threadMethod);
            Name = threadName;
        }

        /// <summary>
        /// ctor.
        /// </summary>
        public NiceThread(ParameterizedThreadStart threadMethod, string threadName)
        {
            _running = true;
            _thread = null;
            _runningMutex = new Mutex();
            _threadHandle = new ManualResetEvent(false);
            _thread = new Thread(threadMethod);
            Name = threadName;
        }

        /// <summary>
        /// Starts the thread.
        /// </summary>
        public void start()
        {
            Debug.Assert(_thread != null);
            Debug.Assert(_thread.IsAlive == false);
            _thread.Start();
        }

        /// <summary>
        /// finalizes the ressources.
        /// </summary>
        public void Dispose()
        {
            Kill();
        }

        /// <summary>
        /// Terminates the thread and blocks until thread has ended.
        /// </summary>
        public void Kill()
        {
            Debug.Assert(_thread != null);
            if (!IsAlive)
                return;

            Running = false;
            
            ThreadUtils.waitForThreadToDie(_thread);
            _runningMutex.Close();
            _threadHandle.Close();
        }

        /// <summary>
        /// Gets or sets the thread's name.
        /// </summary>
        public string Name
        {
            get { return _thread.Name; }
            set { _thread.Name = value; }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this thread is alive.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if (_thread == null || !_running)
                    return false;

                bool handleSignaled = _threadHandle.WaitOne(0, true);
                while (handleSignaled == _thread.IsAlive)
                {
                    Thread.Sleep(0);
                }
                return _thread.IsAlive;
            }
        }
    }
}
