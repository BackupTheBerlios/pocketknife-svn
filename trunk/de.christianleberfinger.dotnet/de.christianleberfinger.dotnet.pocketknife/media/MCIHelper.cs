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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace de.christianleberfinger.dotnet.pocketknife.media
{
    class MCIHelper
    {
        #region WinAPI imports

        /// <summary>
        /// sends a command string to an MCI device.
        /// </summary>
        /// <param name="lpCommand">MCI command string</param>
        /// <param name="lpReturn">A buffer that receives return information. If no return information is needed, this parameter can be null.</param>
        /// <param name="nReturnLength">Number of characters in the return buffer specified by the lpReturn parameter.</param>
        /// <param name="callBack">Handle of a callback window if the notify flag was specified in the command string.</param>
        /// <returns>Returns zero if successful or an error code (int) otherwise. To retrieve a textual description of the return value, pass it to the mciGetErrorString function.</returns>
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        private static extern int mciSendString(string lpCommand, StringBuilder lpReturn, int nReturnLength, IntPtr callBack);


        /// <summary>
        /// Get more detailed information about your MCI error code.
        /// </summary>
        /// <param name="dwError"></param>
        /// <param name="lpstrBuffer"></param>
        /// <param name="uLength"></param>
        /// <returns></returns>
        [DllImport("winmm.dll", EntryPoint = "mciGetErrorStringA", CharSet = CharSet.Ansi)]
        protected static extern int mciGetErrorString(int dwError, StringBuilder lpstrBuffer, int uLength);

        #endregion

        /// <summary>
        /// Internal function to send an MCI command.
        /// </summary>
        /// <param name="command">The MCI command</param>
        /// <returns>The MCI return string.</returns>
        /// <exception cref="MCIException"></exception>
        public static string sendMCICommand(string command)
        {
            StringBuilder buffer = new StringBuilder(255);
            int errorCode = mciSendString(command, buffer, buffer.Capacity, IntPtr.Zero);
            if (errorCode != 0)
            {
                throw new MCIException(errorCode);
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Internal function to send an MCI command.
        /// </summary>
        /// <param name="command">The MCI command</param>
        /// <param name="callbackControl">The control which should notified 
        /// (if the 'notified flag' is set in the command)</param>
        /// <returns>The MCI return string.</returns>
        /// <exception cref="MCIException"></exception>
        public static string sendMCICommand(string command, Control callbackControl)
        {
            IntPtr callbackHandle = IntPtr.Zero;
            if (callbackControl != null)
                callbackHandle = callbackControl.Handle;

            StringBuilder buffer = new StringBuilder(255);
            int errorCode = mciSendString(command, buffer, buffer.Capacity, callbackHandle);
            if (errorCode != 0)
            {
                throw new MCIException(errorCode);
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Get textual information about an MCI error code.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns>A textual representation of the given MCI error code.</returns>
        public static string getMciErrorString(int errorCode)
        {
            StringBuilder buffer = new StringBuilder(256);
            if (mciGetErrorString(errorCode, buffer, buffer.Capacity) == 0)
                return "unknown error.";
            return buffer.ToString();
        }
    }

    /// <summary>
    /// Exception that can occur when using MCI (media control interface)
    /// </summary>
    public class MCIException : Exception
    {
        string _message;
        internal MCIException(int mciErrorCode)
        {
            _message = MCIHelper.getMciErrorString(mciErrorCode);
        }

        /// <summary>
        /// Contains a textual description of the error.
        /// </summary>
        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }

}
