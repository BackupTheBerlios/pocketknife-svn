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

        public ExternalWindow(string windowClass, string windowTitle)
        {
            this.windowClass = windowClass;
            this.windowTitle = windowTitle;
        }

        // Get a handle to an application window.
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public void bringWindowToFront()
        {
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
            SendKeys.Send(key);
        }
    }
}
