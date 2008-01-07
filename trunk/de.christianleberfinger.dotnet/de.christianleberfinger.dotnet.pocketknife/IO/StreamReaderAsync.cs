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

namespace de.christianleberfinger.dotnet.pocketknife.IO
{
        public class StreamReaderAsync
    {
        private Stream inputStream;
        private byte[] buffer = new byte[256];
        private AsyncCallback myCallBack; // delegated method

        public class BytesReceiveEventArgs : EventArgs
        {
            private byte[] _bytes;
            public BytesReceiveEventArgs()
            {
            }
            public byte[] Bytes
            {
                internal set { _bytes = value; }
                get { return _bytes; }
            }
        }

        public class ReadingFinishedEventArgs : EventArgs
        {
            Stream _stream;
            public ReadingFinishedEventArgs(Stream s)
            {
                _stream = s;
            }
            public Stream Stream { get { return _stream; } }
        }
        public delegate void ReadingFinishedHandler(ReadingFinishedEventArgs e);
        public event ReadingFinishedHandler OnReadingFinish;


        public delegate void BytesReceivedHandler(BytesReceiveEventArgs e);
        public event BytesReceivedHandler OnBytesReceive;

        BytesReceiveEventArgs _commonEventArgs = new BytesReceiveEventArgs();
        public StreamReaderAsync(Stream s)
        {
            inputStream = s;
            myCallBack = new AsyncCallback(OnCompletedRead);
        }

        public void start()
        {
            inputStream.BeginRead(buffer, 0, buffer.Length, myCallBack, null);
        }

        void OnCompletedRead(IAsyncResult asyncResult)
        {
            int bytesRead = inputStream.EndRead(asyncResult);
            if (bytesRead > 0)
            {
                invoke(buffer);
                inputStream.BeginRead(buffer, 0, buffer.Length, myCallBack, null);
            }
            else
            {
                ReadingFinishedHandler temp = OnReadingFinish;
                if (temp != null)
                {
                    temp(new ReadingFinishedEventArgs(inputStream));
                }
            }
        }

        private void invoke(byte[] buffer)
        {
            BytesReceivedHandler temp = OnBytesReceive;
            if (temp != null)
            {
                _commonEventArgs.Bytes = buffer;
                temp(_commonEventArgs);
            }
        }

    }
}
