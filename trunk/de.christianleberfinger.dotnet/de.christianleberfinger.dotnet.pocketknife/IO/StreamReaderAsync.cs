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
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife.IO
{
    /// <summary>
    /// This class allows you to read concurrently from a stream without 
    /// caring about threads. There a only few steps to read from a stream:
    /// 1) Create an instance of StreamReaderAsync
    /// 2) Register for the event <see cref="OnBytesReceive"/>
    /// 3) Call the <see cref="start"/> method
    /// </summary>
    public class StreamReaderAsync
    {
        private Stream _stream;
        private byte[] buffer = new byte[256];
        private AsyncCallback myCallBack; // delegated method

        /// <summary>
        /// Is raised when no more bytes could be read from the stream.
        /// </summary>
        public event GenericEventHandler<StreamReaderAsync, ReadingFinishedEventArgs> OnReadingFinish;

        /// <summary>
        /// Is raised when bytes were read from the stream.
        /// </summary>
        public event GenericEventHandler<StreamReaderAsync, BytesReceiveEventArgs> OnBytesReceive;

        /// <summary>
        /// Creates a new instance of StreamReaderAsync.
        /// After creating the instance you have to register for the 
        /// event <see cref="OnBytesReceive"/> and call the <see cref="start"/> method
        /// in order to listen to the given stream.
        /// </summary>
        /// <param name="s"></param>
        public StreamReaderAsync(Stream s)
        {
            Debug.Assert(s!=null, "The given stream may not be null.");
            
            if (s == null)
                throw new ArgumentNullException();

            _stream = s;
            myCallBack = new AsyncCallback(OnCompletedRead);
        }

        /// <summary>
        /// Starts reading from the stream.
        /// You have to register for the event <see cref="OnBytesReceive"/> 
        /// in order to get notified about received bytes.
        /// </summary>
        public void start()
        {
            if (_stream == null)
                throw new ArgumentNullException();
            
            // asynchronen Lesevorgang starten
            IAsyncResult result = _stream.BeginRead(buffer, 0, buffer.Length, myCallBack, null);
        }

        /// <summary>
        /// gets called after an asynchronous reading of the stream was completed
        /// </summary>
        void OnCompletedRead(IAsyncResult asyncResult)
        {
            try
            {
                int byteCount = _stream.EndRead(asyncResult);

                if (byteCount > 0)
                {
                    byte[] readBytes = new byte[byteCount];
                    Buffer.BlockCopy(buffer, 0, readBytes, 0, byteCount);

                    //foreach(byte b in readBytes)
                    //{
                    //    Debug.WriteLine(">>> BYTE: " + b.ToString("X2"));
                    //}

                    //raise bytes read event
                    invoke(readBytes);

                    _stream.BeginRead(buffer, 0, buffer.Length, myCallBack, null);
                }
                else
                {
                    ReadingFinishedEventArgs e = new ReadingFinishedEventArgs(_stream);
                    EventHelper.invoke<StreamReaderAsync, ReadingFinishedEventArgs>(OnReadingFinish, this, e);
                }
            }
            catch (Exception ex)//E/A-Ende Exception
            {
                ReadingFinishedEventArgs e = new ReadingFinishedEventArgs(_stream);
                e.Exception = ex;
                EventHelper.invoke<StreamReaderAsync, ReadingFinishedEventArgs>(OnReadingFinish, this, e);
            }
        }

        /// <summary>
        /// we want to reuse the EventArgs class to reduce the garbage collector's load.
        /// </summary>
        BytesReceiveEventArgs _reusableEventArgs = new BytesReceiveEventArgs();

        private void invoke(byte[] buffer)
        {
            // raise received event
            BytesReceiveEventArgs e = new BytesReceiveEventArgs();
            e.Bytes = buffer;
            //_reusableEventArgs.Bytes = buffer;
            //EventHelper.invoke<StreamReaderAsync, BytesReceiveEventArgs>(OnBytesReceive, this, _reusableEventArgs);
            EventHelper.invoke<StreamReaderAsync, BytesReceiveEventArgs>(OnBytesReceive, this, e);
        }

        /// <summary>
        /// EventArgs class for bytesreceived event
        /// </summary>
        public class BytesReceiveEventArgs : EventArgs
        {
            private byte[] _bytes;
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public BytesReceiveEventArgs()
            {
            }
            /// <summary>
            /// The bytes that were read.
            /// </summary>
            public byte[] Bytes
            {
                internal set { _bytes = value; }
                get { return _bytes; }
            }
        }

        /// <summary>
        /// EventArgs class for reading finished event
        /// </summary>
        public class ReadingFinishedEventArgs : EventArgs
        {
            Stream _stream;
            Exception _exception = null;

            /// <summary>
            /// If an exception caused the reading to quit, you can
            /// see it here.
            /// </summary>
            public Exception Exception
            {
                get { return _exception; }
                set { _exception = value; }
            }

            /// <summary>
            /// Creates a new instance
            /// </summary>
            /// <param name="s"></param>
            public ReadingFinishedEventArgs(Stream s)
            {
                _stream = s;
            }

            /// <summary>
            /// The stream that was read until its end.
            /// You might want to close it.
            /// </summary>
            public Stream Stream { get { return _stream; } }
        }
    }
}
