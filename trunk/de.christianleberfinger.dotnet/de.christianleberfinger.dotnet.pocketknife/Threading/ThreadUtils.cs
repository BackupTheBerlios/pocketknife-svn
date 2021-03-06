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
using System.Threading;
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife.Threading
{
    /// <summary>
    /// Contains some helper methods for handling threads.
    /// </summary>
    public class ThreadUtils
    {
        /// <summary>
        /// Joins a given thread and blocks until it dies.
        /// </summary>
        /// <param name="thread">The thread you want to join.</param>
        public static void waitForThreadToDie(Thread thread)
        {
            if (thread == null)
                return;

            // a thread is not allowed to join itself!
            Debug.Assert(Thread.CurrentThread.ManagedThreadId != thread.ManagedThreadId);

            thread.Join();
        }

        /// <summary>
        /// Joins a given thread and blocks until it dies.
        /// </summary>
        /// <param name="thread">The thread you want to join.</param>
        /// <param name="timeoutMillis"></param>
        public static void waitForThreadToDie(Thread thread, int timeoutMillis)
        {
            if (thread == null)
                return;

            // a thread is not allowed to join itself!
            Debug.Assert(Thread.CurrentThread.ManagedThreadId != thread.ManagedThreadId);

            thread.Join(timeoutMillis);
        }
    }
}
