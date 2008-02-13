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
using System.IO;
using System.Runtime.InteropServices;

namespace de.christianleberfinger.dotnet.pocketknife.IO
{
    /// <summary>
    /// A tool for path things.
    /// </summary>
    public class PathTool
    {
        /// <summary>
        /// The GetShortPathName function obtains the old DOS path form (8.3) of the given input path.
        /// Yes, you're right... DOS is not dead.. :-(
        /// </summary>
        /// <param name="lpLongPath">The long path.</param>
        /// <param name="lpShortPath"></param>
        /// <param name="Buffer"></param>
        /// <returns>If calling this function succeeds, it will return the number of characters 
        /// copied to the given StringBuilder. 
        /// If this number is greater than the length of your given buffer, call the function again
        /// (and be sure to use a buffer big enough).
        /// When the return error is zero, call GetLastError for more information.</returns>
        [DllImport("kernel32")]
        private static extern int GetShortPathName(string lpLongPath, StringBuilder lpShortPath, int Buffer);

        /// <summary>
        /// Converts the given path to 8.3 (old DOS) format.
        /// </summary>
        /// <param name="longPath"></param>
        /// <exception cref="IOException">If something goes wrong.</exception>
        public static string ConvertToDosPath(string longPath)
        {
            StringBuilder filestringBuilder = new StringBuilder(new string(' ', 256));

            int pathStringLength = GetShortPathName(longPath, filestringBuilder, filestringBuilder.Length);

            if (pathStringLength > 0)
            {
                return filestringBuilder.ToString();
            }
            else
            {
                throw new IOException("Error while converting the following path to 8.3 format: " + longPath);
            }
        }

    }
}
