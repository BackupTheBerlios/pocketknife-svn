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

namespace de.christianleberfinger.dotnet.IO
{
    public class SerialPort : System.IO.Ports.SerialPort
    {
        bool reading = false;
        Thread readThread = null;

        public delegate void ByteReceivedHandler(byte receivedByte);
        public event ByteReceivedHandler OnByteReceived;

        public new void Open()
        {
            Close();

            base.Open();

            // Start reading from serialport in a concurrent thread.
            readThread = new Thread(receive);
            readThread.Name = "serialport reading thread";
            readThread.IsBackground = true;
            readThread.Start();
        }

        public new void Close()
        {
            reading = false;

            // Closing the underlying serialport-object throws an IOException in readThread
            // that causes it to interrupt the blocking ReadByte()-call. 
            //  Thus no explicit interruption or abortion of readThread is necessary.
            base.Close();

            // closing the port can take some time
            Thread.Sleep(250);

            // waiting for readThread to die
            Threading.ThreadUtils.waitForThreadToDie(readThread);
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
            }
            catch
            {
                // When the SerialPort and the underlying stream gets closed, an IOException occurs
                // that interrupts the blocking ReadByte()-call.
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

        public void Write(byte[] message)
        {
            Write(message, 0, message.Length);
        }

        public void Write(byte message)
        {
            Write(new byte[]{message});
        }

        public void Write(List<byte> message)
        {
            Write(message.ToArray());
        }

    }
}
