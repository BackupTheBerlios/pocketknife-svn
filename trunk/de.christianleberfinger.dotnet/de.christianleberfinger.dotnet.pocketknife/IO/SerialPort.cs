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
using System.Reflection;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace de.christianleberfinger.dotnet.pocketknife.IO
{
    /// <summary>
    /// Adds some functionality to the SerialPort class of .NET.
    /// </summary>
    public class SerialPort : System.IO.Ports.SerialPort
    {
        bool reading = false;
        Thread readThread = null;

        /// <summary>
        /// Represents the method that handles a received byte from the serial port.
        /// </summary>
        /// <param name="receivedByte"></param>
        public delegate void ByteReceivedHandler(byte receivedByte);

        /// <summary>
        /// Occurs every time when a byte was received from the serial port.
        /// </summary>
        [Description("Is fired every time, when a byte was received from the serial port."),
		 CategoryAttribute("SerialPort")]
        public event ByteReceivedHandler OnByteReceived;

        /// <summary>
        /// Represents the method that handles a changed connection state of the serial port.
        /// </summary>
        /// <param name="connected"></param>
        public delegate void ConnectionStateChangedHandler(bool connected);

        /// <summary>
        /// Occurs whenever the connection state of the serial port has changed.
        /// </summary>
        [Description("Is fired, when a the connection state of the serial port has changed."),
         CategoryAttribute("SerialPort")]
        public event ConnectionStateChangedHandler OnConnectionStateChange;

        /// <summary>
        /// Open the specified port's connection and starts an internal thread for reading from it.
        /// </summary>
        public new void Open()
        {
            Close();

            base.Open();

            // Start reading from serialport in a concurrent thread.
            readThread = new Thread(receive);
            readThread.Name = "serialport (" + PortName + ") reading thread";
            readThread.IsBackground = true;
            readThread.Start();
            fireConnectionStateChanged(OnConnectionStateChange, true);
        }

        /// <summary>
        /// Closes the serial port.
        /// </summary>
        public new void Close()
        {
            reading = false;

            // Closing the underlying serialport-object throws an IOException in readThread
            // that causes it to interrupt the blocking ReadByte()-call. 
            //  Thus no explicit interruption or abortion of readThread is necessary.
            try
            {
                base.Close();

                // closing the port can take some time
                Thread.Sleep(250);
            }
            catch { }

            // waiting for readThread to die
            pocketknife.Threading.ThreadUtils.waitForThreadToDie(readThread, 1000);
            readThread = null;
        }

        /// <summary>
        /// Indicates, whether this serialport is open or closed.
        /// </summary>
        public new bool IsOpen
        {
            get
            {
                return base.IsOpen && reading;
            }
        }

        /// <summary>
        /// Threadstart-method for receiving data from the serial port.
        /// </summary>
        private void receive()
        {
            reading = true;
            try
            {
                while (reading)
                {
                    int? data = -1;

                    data = ReadByte();

                    if (data != null)
                        fireByteReceived(OnByteReceived, (byte)data);
                }
                fireConnectionStateChanged(OnConnectionStateChange, false);
            }
            catch
            {
                // When the SerialPort and the underlying stream gets closed, an IOException occurs
                // that interrupts the blocking ReadByte()-call.
                fireConnectionStateChanged(OnConnectionStateChange, false);
            }
        }

        /// <summary>
        /// Fires the ByteReceived-Event. JIT-inlining is forbidden in order to prevent race conditions.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="receivedByte"></param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void fireByteReceived(ByteReceivedHandler handler, byte receivedByte)
        {
            if (handler != null)
                handler(receivedByte);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void fireConnectionStateChanged(ConnectionStateChangedHandler handler, bool connected)
        {
            if (handler != null)
                handler(connected);
        }

        /// <summary>
        /// Writes a message of bytes to the serial port.
        /// </summary>
        /// <param name="message"></param>
        public void Write(byte[] message)
        {
            Write(message, 0, message.Length);
        }

        /// <summary>
        /// Writes a single byte to the serial port.
        /// </summary>
        /// <param name="message"></param>
        public void Write(byte message)
        {
            Write(new byte[]{message});
        }

        /// <summary>
        /// Writes a message to the serial port that is represented by a list of bytes.
        /// </summary>
        /// <param name="message"></param>
        public void Write(List<byte> message)
        {
            Write(message.ToArray());
        }

    }
}
