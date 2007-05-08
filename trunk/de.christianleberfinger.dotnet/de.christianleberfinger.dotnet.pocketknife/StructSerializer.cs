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
using System.Runtime.InteropServices;

namespace de.christianleberfinger.dotnet.pocketknife
{
    /// <summary>
    /// Provides a generic serializer/deserializer for .NET structs.
    /// </summary>
    /// <typeparam name="T">The type of your struct.</typeparam>
    public class StructSerializer<T>
    {
        /// <summary>
        /// Creates a struct from an array of bytes.
        /// If the number of bytes passed to this function doesn't match the
        /// size of the struct you want to build, a default instance of the
        /// struct is returned.
        /// </summary>
        /// <param name="bytes">The bytes that should build the struct.</param>
        /// <returns>A struct that was built using the given byte array.</returns>
        public static T deserialize(byte[] bytes)
        {
            int structSize = Marshal.SizeOf(typeof(T));
            if (structSize != bytes.Length)
                return default(T);

            IntPtr buffer = Marshal.AllocHGlobal(structSize);
            Marshal.Copy(bytes, 0, buffer, structSize);
            T retobj = (T)Marshal.PtrToStructure(buffer, typeof(T));
            Marshal.FreeHGlobal(buffer);
            return retobj;
        }

        /// <summary>
        /// Serializes a given struct to an array of bytes, so that it can be easily 
        /// transferred over streams such as file streams or network streams.
        /// </summary>
        /// <param name="yourStruct">The struct you want to serialize.</param>
        /// <returns>An array of bytes.</returns>
        public static byte[] serialize(T yourStruct)
        {
            int structSize = Marshal.SizeOf(yourStruct);
            IntPtr buffer = Marshal.AllocHGlobal(structSize);
            Marshal.StructureToPtr(yourStruct, buffer, false);
            byte[] bytes = new byte[structSize];
            Marshal.Copy(buffer, bytes, 0, structSize);
            Marshal.FreeHGlobal(buffer);
            return bytes;
        }
    }
}
