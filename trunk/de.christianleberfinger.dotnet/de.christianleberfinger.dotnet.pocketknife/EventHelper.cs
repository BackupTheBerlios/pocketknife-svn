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
        /// Unsafe means: the given arguments aren't checked for type safety (as they are objects)
        /// </summary>
        /// <param name="delegateToInvoke"></param>
        /// <param name="args"></param>
        public static void invokeUnsafe(Delegate delegateToInvoke, params object[] args)
        {
            if (delegateToInvoke == null)
                return;

            Delegate[] delegates = delegateToInvoke.GetInvocationList();
            foreach (Delegate del in delegates)
            {
                try
                {
                    del.DynamicInvoke(args);
                }
                catch(Exception ex) 
                {

                    Console.WriteLine("Error calling: " + del.Method.ToString() + " in " + del.Target.ToString());
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        public static void invoke(GenericEventHandler eh)
        {
            invokeUnsafe(eh);
        }

        public static void invoke<T>(GenericEventHandler<T> eh, T t)
        {
            invokeUnsafe(eh, t);
        }

        public static void invoke<T,U>(GenericEventHandler<T, U> eh, T t, U u)
        {
            invokeUnsafe(eh, t, u);
        }

        public static void invoke<T,U,V>(GenericEventHandler<T, U, V> eh, T t, U u, V v)
        {
            invokeUnsafe(eh, t, u, v);
        }

        public static void invoke<T,U,V,W>(GenericEventHandler<T, U, V, W> eh, T t, U u, V v, W w)
        {
            invokeUnsafe(eh, t, u, v, w);
        }

        public static void invoke<T,U,V,W,X>(GenericEventHandler<T, U, V, W, X> eh, T t, U u, V v, W w, X x)
        {
            invokeUnsafe(eh, t, u, v, w, x);
        }

        public static void invoke<T,U,V,W,X,Y>(GenericEventHandler<T, U, V, W, X, Y> eh, T t, U u, V v, W w, X x, Y y)
        {
            invokeUnsafe(eh, t, u, v, w, x, y);
        }
    }

    /// <summary>
    /// parameterless handler
    /// </summary>
    public delegate void GenericEventHandler();

    /// <summary>
    /// handler with 1 parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public delegate void GenericEventHandler<T>(T t);

    /// <summary>
    /// handler with 2 parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="t"></param>
    /// <param name="u"></param>
    public delegate void GenericEventHandler<T, U>(T t, U u);

    /// <summary>
    /// handler with 3 parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="t"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    public delegate void GenericEventHandler<T, U, V>(T t, U u, V v);

    /// <summary>
    /// handler with 3 parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="t"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    public delegate void GenericEventHandler<T, U, V, W>(T t, U u, V v, W w);

    /// <summary>
    /// handler with 4 parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <param name="t"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <param name="x"></param>
    public delegate void GenericEventHandler<T, U, V, W, X>(T t, U u, V v, W w, X x);

    /// <summary>
    /// handler with 5 parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <param name="t"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public delegate void GenericEventHandler<T, U, V, W, X, Y>(T t, U u, V v, W w, X x, Y y);
}
