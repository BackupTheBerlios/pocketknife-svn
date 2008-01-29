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
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace de.christianleberfinger.dotnet.pocketknife
{
    /// <summary>
    /// Helper class for calling events. 
    /// Inspired by "Programming .NET Components" by Juval Löwy.
    /// </summary>
    public class EventHelper
    {
        private delegate void TestEventHandler(string a, int b);
        private event TestEventHandler TestEvent;

        /// <summary>
        /// just for comparison
        /// </summary>
        private void basicInvoke()
        {
            TestEventHandler temp = TestEvent;
            if (temp != null)
            {
                temp(null, 0);
            }
        }

        /// <summary>
        /// Exception can occur when you invoke events.
        /// When you have multiple subscribers, this class 
        /// collects the exceptions.
        /// </summary>
        public class InvokeException : Exception
        {
            List<Exception> _exceptions = null;
            /// <summary>
            /// Creates a new instance of <see cref="InvokeException"/>
            /// </summary>
            /// <param name="exceptions"></param>
            public InvokeException(List<Exception> exceptions)
            {
                _exceptions = exceptions;
            }
            /// <summary>
            /// A list of all exceptions that were thrown during invoking all the subscribers
            /// of the event.
            /// </summary>
            public List<Exception> Exceptions
            {
                get { return _exceptions; }
            }
        }

        /// <summary>
        /// Unsafe means here: the given arguments aren't checked for type safety (as they are objects).
        /// Consider using a <see cref="GenericEventHandler&lt;SENDER,ARGS&gt;"/>
        /// </summary>
        /// <param name="delegateToInvoke"></param>
        /// <param name="args"></param>
        /// <exception cref="InvokeException">When an error occured in one or more delegates,
        /// this exception is thrown that contains all collected exceptions during invoking.
        /// </exception>
        /// <remarks>All delegates will be invoked even if exceptions occur during ivoking.</remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void invokeUnsafe(Delegate delegateToInvoke, params object[] args)
        {
            if (delegateToInvoke == null)
                return;

            Delegate[] delegates = delegateToInvoke.GetInvocationList();
            List<Exception> occuredExceptions = null;
            foreach (Delegate del in delegates)
            {
                try
                {
                    if(del!=null)
                        del.DynamicInvoke(args);
                }
                catch(Exception ex) 
                {
                    if (occuredExceptions == null)
                    {
                        occuredExceptions = new List<Exception>();
                    }
                    occuredExceptions.Add(ex);                    
                }
            }
            if (occuredExceptions != null)
            {
                throw new InvokeException(occuredExceptions);
            }
        }

        /// <summary>
        /// Type safe invoke
        /// </summary>
        /// <typeparam name="SENDER">The type of the event source.</typeparam>
        /// <typeparam name="ARGS">The type of the event arguments.</typeparam>
        /// <param name="delegat">The event you want to invoke.</param>
        /// <param name="sender">Any object that is the source of this event. Don't send null as it might be not expected by the user code.</param>
        /// <param name="args">Event arguments.</param>
        /// <exception cref="InvokeException">When an error occured in one or more delegates,
        /// this exception is thrown that contains all collected exceptions during invoking.
        /// </exception>
        /// <remarks>All delegates will be invoked even if exceptions occur during ivoking.</remarks>
        public static void invoke<SENDER,ARGS>(GenericEventHandler<SENDER, ARGS> delegat, SENDER sender, ARGS args) where ARGS:EventArgs
        {
            invokeUnsafe(delegat, sender, args);
        }
    }

    /// <summary>
    /// Generic event handler.
    /// </summary>
    /// <typeparam name="SENDER">The type of the event source.</typeparam>
    /// <typeparam name="ARGS">The type of the event arguments.</typeparam>
    /// <param name="sender">Any object that is the source of this event. Don't send null as it might be not expected by the user code.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void GenericEventHandler<SENDER, ARGS>(SENDER sender, ARGS e);

}
