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

namespace de.christianleberfinger.dotnet.pocketknife.Net
{
    /// <summary>
    /// Delegate for the change of the connection's state.
    /// </summary>
    public delegate void ConnectionEventHandler(ITcpClient connection);

    /// <summary>
    /// Delegate for message received
    /// </summary>
    /// <param name="connection">The connection that was the message's source.</param>
    /// <param name="message">The message that was received.</param>
    public delegate void MessageReceivedEventHandler(ITcpClient connection, string message);

    /// <summary>
    /// Delegate for handling occured exceptions.
    /// </summary>
    /// <param name="connection">The connection it that the exception occured.</param>
    /// <param name="exception">The exception that occured.</param>
    public delegate void ExceptionOccuredEventHandler(ITcpClient connection, Exception exception);

    ///<summary>
    /// Interface for easy-to-use TCP clients.
    /// </summary>
    public interface ITcpClient
    {
        /// <summary>
        /// Connects the client to a remote TCP host using the specified host name and port number.
        /// </summary>
        void connect(string hostName, int port);

        /// <summary>
        /// Gets a boolean value that determines whether this client is connected to a remote host.
        /// </summary>
        /// <value></value>
        bool Connected { get; }

        /// <summary>
        /// Occurs when the connection status changes, e.g. from Disconnected to Connected
        /// </summary>
        event ConnectionEventHandler ConnectionStateChanged;

        /// <summary>
        /// Disconnects the client from the remote device.
        /// </summary>
        /// <returns></returns>
        void disconnect();

        /// <summary>
        /// Gets or sets the encoding used when communicating with the remote device.
        /// </summary>
        System.Text.Encoding Encoding { get; set; }

        /// <summary>
        /// Occurs with an Exception during communication.
        /// </summary>
        event ExceptionOccuredEventHandler ExceptionOccured;

        /// <summary>
        /// Occurs when a message is received from the remote device.
        /// </summary>
        event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Sends the given string in the set encoding.
        /// </summary>
        void send(string message);

        /// <summary>
        /// Sends the given byte array.
        /// </summary>
        /// <param name="byteData"></param>
        void send(byte[] byteData);

        /// <summary>
        /// Gets or sets the delimiter for delimiting the messages from each other. Default is line-break '\n'.
        /// </summary>
        string Delimiter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a boolean value that determines whether the TcpClient should try to reconnect a lost connection.
        /// </summary>
        bool AutoReconnect { get; set; }
    }
}
