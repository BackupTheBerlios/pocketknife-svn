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
using System.Windows.Forms;

namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    /// <summary>
    /// Allows you to send simulated Key-Pressed-Events to an external Application.
    /// Attention: This class is definitely not in a final and well tested state.
    /// </summary>
    public class ExternalWindow
    {
        string windowClass = null;
        string windowTitle = null;

        /// <summary>
        /// Creates an instance, that sends the keyboard events "blindly".
        /// That means, it doesn't send it to a specified windows, but to
        /// the window that is active at the moment.
        /// </summary>
        public ExternalWindow()
        {
        }

        /// <summary>
        /// Sends the keyboard events to a specified window. This is
        /// achieved by first bringing the specified window to the front 
        /// and sending the keyboard event afterwards.
        /// </summary>
        /// <param name="windowClass">Class of the specified window.
        /// HINT:
        /// You can discover the correct setting by using Spy++.</param>
        /// <param name="windowTitle">Title of the specified window.
        /// HINT:
        /// You can discover the correct setting by using Spy++.</param>
        public ExternalWindow(string windowClass, string windowTitle)
        {
            this.windowClass = windowClass;
            this.windowTitle = windowTitle;
        }

        /// <summary>
        /// Get a handle to an application window.<para/>
        /// </summary>
        /// <param name="lpClassName">Class of the specified window.
        /// HINT:
        /// You can discover the correct setting by using Spy++.</param>
        /// <param name="lpWindowName">Title of the specified window.
        /// HINT:
        /// You can discover the correct setting by using Spy++.</param>
        /// <returns>window handle as int pointer</returns>
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        /// <summary>
        /// Sets the specified window to be the active window.
        /// </summary>
        /// <param name="hWnd">The handle of the window.</param>
        /// <returns>A flag indicating the operation's success.</returns>
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public void bringWindowToFront()
        {
            if (windowClass == null || windowTitle == null)
                return;

            IntPtr hWnd = FindWindow(windowClass, windowTitle);
            SetForegroundWindow(hWnd);
        }

        public void sendKey(Keys key)
        {
            bringWindowToFront();
            SendKeys.Send(key.ToString());
        }

        public void sendKey(string key)
        {
            bringWindowToFront();
            SendKeys.Send(key);
        }
    }
}
