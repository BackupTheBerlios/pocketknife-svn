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
        /// Defines exceptionBehaviour for invoking an event.
        /// </summary>
        [Flags]
        public enum ExceptionOptions : byte
        {
            /// <summary>
            /// Ignores exceptions. That means, invoking will be continued and no exception will be thrown to caller.
            /// </summary>
            IgnoreExceptions = 0,

            /// <summary>
            /// Cancels calling pending subscribers when the first of them throws an exception.
            /// </summary>
            CancelInvokeAtException = 1,

            /// <summary>
            /// Throws an InvokeException if one of the subscribers throwed an exception.
            /// </summary>
            ThrowException = 2
        }

        /// <summary>
        /// checks whether a given option is. for internal use
        /// </summary>
        /// <param name="o"></param>
        /// <param name="optionToQuery"></param>
        /// <returns></returns>
        private static bool optionIsSet(ExceptionOptions o, ExceptionOptions optionToQuery)
        {
            return (o & optionToQuery) == optionToQuery;
        }

        /// <summary>
        /// Unsafe means here: the given arguments aren't checked for type safety (as they are objects).
        /// Consider defining events using a <see cref="GenericEventHandler&lt;SENDER,ARGS&gt;"/>
        /// </summary>
        /// <param name="delegateToInvoke">The delegate that should be invoked.</param>
        /// <param name="args">The delegate's arguments.</param>
        /// <exception cref="InvokeException">When an error occured in one or more delegates,
        /// this exception is thrown that contains all collected exceptions during invoking.
        /// </exception>
        /// <remarks>All delegates will be invoked even if exceptions occur during invoking.</remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void invokeUnsafe(Delegate delegateToInvoke, params object[] args)
        {
            invokeUnsafe(delegateToInvoke, ExceptionOptions.IgnoreExceptions, args);
        }

        /// <summary>
        /// Invokes an event. You further can specify the action that will be taken when an exception 
        /// occurs in the invoked code.
        /// Consider defining events using a <see cref="GenericEventHandler&lt;SENDER,ARGS&gt;"/>
        /// </summary>
        /// <param name="delegateToInvoke">The event you want to invoke.</param>
        /// <param name="args">Event arguments.</param>
        /// <param name="exceptionBehaviour">Defines what shall happen with catched exceptions.</param>
        /// <remarks>No exception is thrown when using this method. If you want to be informed about exceptions in invoked code,
        /// use overloaded method. All delegates will be invoked even if exceptions occur during ivoking.</remarks>
        /// <exception cref="InvokeException">If you activated exception throwing and an error occured in one or more delegates, an
        /// <see cref="InvokeException"/>Is thrown that contains all collected exceptions during invoking.</exception>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void invokeUnsafe(Delegate delegateToInvoke, ExceptionOptions exceptionBehaviour, params object[] args)
        {
            if (delegateToInvoke == null)
                return;

            Delegate[] delegates = delegateToInvoke.GetInvocationList();
            List<Exception> occuredExceptions = null;
            foreach (Delegate del in delegates)
            {
                try
                {
                    if (del != null)
                        del.DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    if (optionIsSet(exceptionBehaviour, ExceptionOptions.ThrowException))
                    {
                        if (occuredExceptions == null)
                        {
                            occuredExceptions = new List<Exception>();
                        }
                        occuredExceptions.Add(ex);
                    }

                    if(optionIsSet(exceptionBehaviour, ExceptionOptions.CancelInvokeAtException))
                        break;
                }
            }
            if (occuredExceptions != null)
            {
                throw new InvokeException(occuredExceptions);
            }
        }

        /// <summary>
        /// Type safe invoke. This is the recommended method to raise events.
        /// </summary>
        /// <typeparam name="SENDER">The type of the event source.</typeparam>
        /// <typeparam name="ARGS">The type of the event arguments.</typeparam>
        /// <param name="delegat">The event you want to invoke.</param>
        /// <param name="sender">Any object that is the source of this event. Don't send null as it might be not expected by the user code.</param>
        /// <param name="args">Event arguments.</param>
        /// <remarks>No exception is thrown when using this method. If you want to be informed about exceptions in invoked code,
        /// use overloaded method. All delegates will be invoked even if exceptions occur during ivoking.</remarks>
        public static void invoke<SENDER, ARGS>(GenericEventHandler<SENDER, ARGS> delegat, SENDER sender, ARGS args) where ARGS : EventArgs
        {
            invokeUnsafe(delegat, ExceptionOptions.IgnoreExceptions, sender, args);
        }

        /// <summary>
        /// Type safe invoke. This is the recommended method to raise events. You can further specify the
        /// action that will be taken when an exception occurs in the invoked code.
        /// </summary>
        /// <typeparam name="SENDER">The type of the event source.</typeparam>
        /// <typeparam name="ARGS">The type of the event arguments.</typeparam>
        /// <param name="delegat">The event you want to invoke.</param>
        /// <param name="sender">Any object that is the source of this event. Don't send null as it might be not expected by the user code.</param>
        /// <param name="args">Event arguments.</param>
        /// <param name="exceptionBehaviour">Defines what shall happen with catched exceptions.</param>
        /// <remarks>No exception is thrown when using this method. If you want to be informed about exceptions in invoked code,
        /// use overloaded method. All delegates will be invoked even if exceptions occur during ivoking.</remarks>
        public static void invoke<SENDER, ARGS>(GenericEventHandler<SENDER, ARGS> delegat, ExceptionOptions exceptionBehaviour, SENDER sender, ARGS args) where ARGS : EventArgs
        {
            invokeUnsafe(delegat, exceptionBehaviour, sender, args);
        }
    }

    /// <summary>
    /// Generic event handler. Use this delegate to declare type safe events.
    /// </summary>
    /// <typeparam name="SENDER">The type of the event source.</typeparam>
    /// <typeparam name="ARGS">The type of the event arguments.</typeparam>
    /// <param name="sender">Any object that is the source of this event. Don't send null as it might be not expected by the user code.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void GenericEventHandler<SENDER, ARGS>(SENDER sender, ARGS e);

}
