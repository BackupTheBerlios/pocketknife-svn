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
        /// Class of the specified window. If set to NULL, all class names match.
        /// HINT: You can discover the correct setting by using Spy++.
        /// </summary>
        public string WindowClass
        {
            get { return windowClass; }
            set { windowClass = value; }
        }

        /// <summary>
        /// Title of the specified window. If set to NULL, all window titles match.
        /// HINT: You can discover the correct setting by using Spy++.
        /// </summary>
        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; }
        }

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
        /// <param name="windowClass">Class of the specified window. If set to NULL, 
        /// all class names match. 
        /// HINT: You can discover the correct setting by using Spy++.</param>
        /// <param name="windowTitle">Title of the specified window. If set to NULL, 
        /// all window titles match.
        /// HINT: You can discover the correct setting by using Spy++.</param>
        public ExternalWindow(string windowClass, string windowTitle)
        {
            this.windowClass = windowClass;
            this.windowTitle = windowTitle;
        }

        /// <summary>
        /// Get a handle to an application window.<para/>
        /// </summary>
        /// <param name="windowClass">Class of the specified window. If set to NULL, 
        /// all class names match.
        /// HINT:
        /// You can discover the correct setting by using Spy++.</param>
        /// <param name="windowTitle">Title of the specified window. If set to NULL, 
        /// all window titles match.
        /// HINT:
        /// You can discover the correct setting by using Spy++.</param>
        /// <returns>window handle as int pointer</returns>
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string windowClass,
            string windowTitle);

        /// <summary>
        /// Sets the specified window to be the active window.
        /// </summary>
        /// <param name="hWnd">The handle of the window.</param>
        /// <returns>A flag indicating the operation's success.</returns>
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Activates the window that matches the current setting of
        /// window class and window title.
        /// </summary>
        public void bringWindowToFront()
        {
            IntPtr windowHandle = findWindow();

            if (windowHandle == IntPtr.Zero)
                return;

            SetForegroundWindow(windowHandle);
        }

        /// <summary>
        /// Searches for a window that matches the current setting of
        /// window class and window title.
        /// </summary>
        /// <returns>An int pointer representing the found window or 
        /// IntPtr.Zero if no adequate window was found.</returns>
        IntPtr findWindow()
        {
            return FindWindow(windowClass, windowTitle);
        }

        /// <summary>
        /// That's just a dummy method in order to pay attention to the Keys class.
        /// Unfortunately there's no easy way to use string enums in C#.
        /// But a possible way is presented in http://www.codeproject.com/csharp/StringEnum.asp
        /// </summary>
        /// <param name="key"></param>
        public void sendKey(Keys key)
        {
            throw new NotImplementedException("Use sendKey(string)");
        }

        /// <summary>
        /// Sends the keys you define. If you want to know how to create special keys or key combinations
        /// not defined in the class ExternalWindow.Keys please have a look at the microsoft msdn homepage.
        /// <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.sendkeys.send.aspx"/>
        /// </summary>
        /// <param name="key">The key (or combination of keys) to send to the external application.</param>
        public void sendKey(string key)
        {
            IntPtr windowHandle = findWindow();

            if (windowHandle == IntPtr.Zero)
                return;

            bringWindowToFront();

            SendKeys.SendWait(key);

            bringWindowToFront();
        }

        /// <summary>
        /// Defines some Keys you are able to send to an external application.
        /// You can also combine the keys.
        /// </summary>
        public abstract class Keys
        {
            public const string BACKSPACE = "{BACKSPACE}";
            public const string BREAK = "{BREAK}";
            public const string CAPS_LOCK = "{CAPSLOCK}";
            public const string DELETE = "{DELETE}";
            public const string DOWN = "{DOWN}";
            public const string END = "{END}";
            public const string ENTER = "{ENTER}";
            public const string ESCAPE = "{ESC}";
            public const string HELP = "{HELP}";
            public const string HOME = "{HOME}";
            public const string INSERT = "{INSERT}";
            public const string LEFT = "{LEFT}";
            public const string NUM_LOCK = "{NUMLOCK}";
            public const string PAGE_DOWN = "{PGDN}";
            public const string PAGE_UP = "{PGUP}";
            public const string RIGHT = "{RIGHT}";
            public const string SCROLL_LOCK = "{SCROLLLOCK}";
            public const string TAB = "{TAB}";
            public const string UP = "{UP}";
            public const string F1 = "{F1}";
            public const string ADD = "{ADD}";

            /// <summary>
            /// reserved for future use
            /// </summary>
            public const string PRINT_SCREEN = "{PRTSC}";

            /// <summary>
            /// The subtract key on the numeric keyboard.
            /// </summary>
            public const string SUBTRACT = "{SUBTRACT}";

            /// <summary>
            /// The multiply key on the numeric keyboard.
            /// </summary>
            public const string MULTIPLY = "{MULTIPLY}";

            /// <summary>
            /// The divide key on the numeric keyboard.
            /// </summary>
            public const string DIVIDE = "{DIVIDE}";

            public const string SHIFT = "+";
            public const string CONTROL = "^";
            public const string ALT = "%";

            public const string CURLYBRACE_OPEN = "{{}";
            public const string CURLYBRACE_CLOSE = "{}}";
        }
    }
}
