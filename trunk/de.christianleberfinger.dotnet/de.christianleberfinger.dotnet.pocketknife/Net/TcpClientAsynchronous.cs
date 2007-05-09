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
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace de.christianleberfinger.dotnet.pocketknife.Net
{
    ///<summary>
    /// Offers a simple way to communicate with a TCP remote device.
    /// You don't have to care about the asynchronous communication in detail.
    /// The only thing to do is opening a connection and send messages or wait for received messages.
    /// All callbacks are handled by few events.
    /// Received messages are returned without \r\n on their ends.
    /// </summary>
    ///  <example>This sample shows how to set up a simple TCP client reading the homepage of your local webserver.
    /// <code>
    ///
    ///    static void Main(string[] args)
    ///    {
    ///        IClient client = new AsynchronousClient("localhost", 80);
    ///        client.ConnectionStateChanged += new AsynchronousClient.ConnectionEventHandler(client_ConnectionStateChanged);
    ///        client.MessageReceived += new AsynchronousClient.MessageReceivedEventHandler(client_MessageReceived);
    ///        client.ExceptionOccured += new AsynchronousClient.ExceptionOccuredEventHandler(client_ExceptionOccured);
    ///
    ///        client.connect();
    ///
    ///        Console.ReadLine();
    ///    }
    ///
    /// </code></example>
    ///  <example>The following lines define the callback-methods:
    /// <code>
    ///
    ///     static void client_ExceptionOccured(IClient connection, Exception exception)
    ///     {
    ///         Console.WriteLine(exception.ToString());
    ///     }
    ///
    ///     static void client_MessageReceived(IClient connection, string message)
    ///     {
    ///         Console.WriteLine("Received: " + message);
    ///     }
    ///
    ///     static void client_ConnectionStateChanged(IClient connection)
    ///     {
    ///         Console.WriteLine("Connected: " + connection.Connected);
    ///         if (connection.Connected)
    ///          {
    ///             connection.send("GET /");
    ///         }
    ///      }
    /// </code></example>
    public class TcpClientAsynchronous : ITcpClient
    {

        /**********/
        /* Events */
        /**********/

        /// <summary>
        /// Occurs when the connection status changes, e.g. from Disconnected to Connected
        /// </summary>
        public event ConnectionEventHandler ConnectionStateChanged;

        /// <summary>
        /// Occurs when a message is received from the remote device.
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Occurs with an Exception during communication.
        /// </summary>
        public event ExceptionOccuredEventHandler ExceptionOccured;

        /**********/
        /* Fields */
        /**********/
        private int _port;
        private string _host;
        private Socket _socket;

        private string _msgDelimiter = "\n";
        private Encoding _encoding = Encoding.Default;

        private bool _autoReconnect = false;
        private Timer _reconnectTimer;

        // autoreconnect muss nur durchgeführt werden, nachdem einmal connect() 
        // und noch nicht explizit disconnect() aufgerufen wurde.
        private bool _tryAutoReconnect = false;

        // Signalisiert, dass der Connect-Vorgang abgeschlossen ist.
        // Wird hier eigentlich nicht mehr benötigt, ich lasse es aber als Beispiel mal stehen.
        private ManualResetEvent _connectDone = new ManualResetEvent(false);

        // Um asynchron von einem Socket lesen zu können, müssen Daten zwischen den 
        // Aufrufen von BeginReceive gespeichert werden, die mehrfach auftreten können.
        private class ReceiveStateObject
        {
            // Das Socket
            public Socket socket = null;
            // Größe des Empfangs-Puffers
            public const int BUFFER_SIZE = 256;
            // Empfangspuffer zum lesen aus dem Stream
            public byte[] buffer = new byte[BUFFER_SIZE];
            // StringBuilder zum konkatenieren der empfangenen Nachrichten
            public StringBuilder sb = new StringBuilder();
        }

        // Stellt den Sende-Zustand dar. Wird benötigt, um festzustellen, dass beim 
        // EndSend-Vorgang auch wirklich der komplette Puffer übertragen wurde.
        private class SendStateObject
        {
            // Das Socket
            public Socket socket = null;
            // Die Daten, die gesendet werden sollen
            public byte[] data = null;
            // Die Anzahl an bytes, die bereits gesendet wurden
            public int bytesSend = 0;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz von AsynchronousConnection.
        /// </summary>
        /// <param name="host">Der Host-Name des TCP-Servers, z.B.: "localhost"</param>
        /// <param name="port">Der Port am TCP-Server, z.B. 80</param>
        public TcpClientAsynchronous(string host, int port)
        {
            this._port = port;
            this._host = host;

            // Das TCP/IP-Socket initialisieren.
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Timer für Auto-Reconnect
            _reconnectTimer = new Timer(new TimerCallback(autoReconnectTimerCallback), this, 1000, 1000);
        }

        #region ITcpClient Members

        /// <summary>
        /// Tritt eine Exception auf, so sollte diese an den Callback-Handler weitergereicht werden 
        /// und die Verbindung geschlossen werden. Dies kann bequem durch einen Aufruf von handleException() 
        /// geschehen.
        /// </summary>
        /// <param name="e">Die übergebene Exception</param>
        private void handleException(Exception e)
        {
            if (ExceptionOccured != null)
                ExceptionOccured(this, e);
        }

        /// <summary>
        /// Connects the client to a remote TCP host using the specified host name and port number.
        /// </summary>
        public void connect()
        {
            _tryAutoReconnect = true;

            // Nichts zu tun, wenn bereits verbunden
            if (_socket.Connected)
                return;

            IPEndPoint remoteEP = null;
            try
            {
                // Den übergebenen Hostnamen auflösen
                IPAddress ipAddress = Dns.GetHostAddresses(_host)[0];
                remoteEP = new IPEndPoint(ipAddress, _port);

                // Den Verbindungsaufbau anstoßen
                _socket.BeginConnect(remoteEP, new AsyncCallback(connectCallback), _socket);
            }
            catch (System.ObjectDisposedException)
            {
                // Wurde das Socket bereits disposed, so muss ein neues erstellt werden
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Den Verbindungsaufbau anstoßen
                _socket.BeginConnect(remoteEP, new AsyncCallback(connectCallback), _socket);
            }
            catch (Exception e)
            {
                handleException(e);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Connect-Vorgang abgeschlossen ist.
        /// </summary>
        /// <param name="result"></param>
        private void connectCallback(IAsyncResult result)
        {
            try
            {
                // Socket aus dem Async-Result holen.
                Socket client = (Socket)result.AsyncState;

                // Verbindungsaufbau abschließen
                client.EndConnect(result);

                // Das Empfangen von Daten anstoßen
                receive(client);

                // Dem Prozess den erfolgreichen Verbindungsaufbau signalisieren.
                _connectDone.Set();

                /* 
                 * OnConnectionStateChanged - Event feuern.
                 * Es können Exceptions in einer darüberliegenden Schicht bei der Bearbeitung des Events auftreten, z.B.
                 * häufig ein threadübergreifender Zugriff auf ein GUI-Element. Damit diese Exception den Verbindungsaufbau
                 * nicht unterbricht, wird der ConnectionStateChanged-Event erst ganz am Ende propagiert.
                 */
                ConnectionEventHandler handler = ConnectionStateChanged;
                if (handler != null)
                {
                    handler(this);
                }
            }
            catch (Exception e)
            {
                handleException(e);
            }
        }

        /// <summary>
        /// </summary>
        /// <value>bool</value>
        public bool Connected
        {
            get
            {
                return _socket.Connected;
            }
        }

        /// <summary>
        /// Erlaubt es, die Encodierungs-Variante für den Datenaustausch abzufragen und zu ändern.
        /// Standardmäßig wird Encoding.Default benutzt, die Standard-ANSI-code-page des Systems.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return _encoding;
            }
            set
            {
                _encoding = value;
            }
        }

        /// <summary>
        /// Gets or sets the delimiter for delimiting the messages from each other. Default is line-break '\n'.
        /// </summary>
        public string Delimiter
        {
            get
            {
                return this._msgDelimiter;
            }
            set
            {
                this._msgDelimiter = value;
            }
        }

        private void closeSocket()
        {
            try
            {
                // Das Socket schließen
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();

                // OnConnectionStateChanged - Event feuern
                if (ConnectionStateChanged != null)
                    ConnectionStateChanged(this);
            }
            // Eine Exception beim Socket-Schließen kann meines Erachtens vernachlässigt werden
            catch { }
        }

        /// <summary>
        /// Trennt die Socket-Verbindung.
        /// </summary>
        public void disconnect()
        {
            _tryAutoReconnect = false;

            // Es ist nichts zu tun, wenn diese Verbindung bereits unterbrochen ist.
            if (!Connected || _socket == null)
            {
                return;
            }

            closeSocket();
        }

        /// <summary>
        /// Sendet eine Nachricht an den Client. 
        /// Die Nachricht wird automatisch um den Message-Delimiter erweitert.
        /// </summary>
        /// <param name="message">Die Nachricht, die gesendet werden soll.</param>
        public void send(string message)
        {
            // Den String zum Senden in ein byte-Array umwandeln 
            byte[] byteData = _encoding.GetBytes(message + _msgDelimiter);

            // Den asynchronen Sendevorgang anstoßen.
            try
            {
                SendStateObject state = new SendStateObject();
                state.socket = _socket;
                state.data = byteData;

                _socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(sendCallback), state);
            }
            catch (SocketException e)
            {
                handleException(e);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn Daten gesendet werden.
        /// </summary>
        /// <param name="ar"></param>
        private void sendCallback(IAsyncResult ar)
        {
            try
            {
                SendStateObject state = (SendStateObject)ar.AsyncState;

                // Socket aus state-Objekt holen.
                Socket client = state.socket;

                byte[] byteData = state.data;

                // EndSend schickt (zumindest) einen Teil des Puffers 
                // und gibt die Anzahl der gesendeten Bytes zurück.
                int bytesSent = state.bytesSend + client.EndSend(ar);

                // Einen neuen Sendevorgang anstoßen, wenn nicht alle Daten gesendet wurden.
                // Bei meinen Tests (mit bis zu 1 Mio. bytes) war das zwar nie der Fall, jedoch geht die 
                // MSDN-Dokumentation davon aus, dass dieser Fall eintreten kann:
                // "If the return value from EndSend indicates that the buffer was not completely sent, 
                // call the BeginSend method again, modifying the buffer to hold the unsent data."

                if (bytesSent < byteData.Length)
                {
                    // Anzahl der bereits gesendeten bytes im State ablegen
                    state.bytesSend = bytesSent;

                    // Sendevorgang anstoßen
                    client.BeginSend(byteData, bytesSent, byteData.Length, SocketFlags.None,
                    new AsyncCallback(sendCallback), state);
                }

            }
            catch (Exception e)
            {
                handleException(e);
            }
        }

        /// <summary>
        /// Sets up the state object and then calls the BeginReceive method to
        /// read the data from the client socket asynchronously.
        /// </summary>
        /// <param name="client">The socket</param>
        private void receive(Socket client)
        {
            try
            {
                // Create the state object.
                ReceiveStateObject state = new ReceiveStateObject();
                state.socket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, ReceiveStateObject.BUFFER_SIZE, 0,
                    new AsyncCallback(receiveCallback), state);
            }
            catch (Exception e)
            {
                handleException(e);
            }
        }

        private void receiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                ReceiveStateObject state = (ReceiveStateObject)ar.AsyncState;
                Socket client = state.socket;

                // Daten lesen, die vom Client gesendet wurden
                int bytesRead = 0;
                try
                {
                    bytesRead = client.EndReceive(ar);
                }
                // Disconnect an CallbackHandler weiterreichen, falls die
                // Verbindung zum Client getrennt wurde.
                catch (Exception)
                {
                    closeSocket();
                    return;
                }

                if (bytesRead == 0)
                {
                    closeSocket();
                    return;
                }

                // Empfangene Nachricht im StringBuilder zur Weiterverarbeitung ablegen 
                state.sb.Append(_encoding.GetString(state.buffer, 0, bytesRead));

                string received = state.sb.ToString();

                // Wurde (zumindest eine) vollständige Nachricht empfangen ?
                if (received.Contains(_msgDelimiter))
                {
                    string[] messages = received.Split(_msgDelimiter[0]);

                    // endet die receivedMessage noch nicht mit dem delimiter, 
                    // so wurde (zumindest der letzte Teil) noch nicht komplett übertragen 
                    // -> letzten Teil der receivedMessage für nächsten Callback merken
                    if (!received.EndsWith(_msgDelimiter))
                    {
                        // Letzte Nachricht im StringBuilder ablegen
                        state.sb = new StringBuilder(messages[messages.Length - 1]);
                    }
                    else
                    {
                        // Stringbuilder zurücksetzen
                        state.sb = new StringBuilder();
                    }

                    // Ist der EventHandler null?
                    if (MessageReceived == null)
                        return;

                    // Alle Messages bis auf die letzte an den Callback-Handler übergeben.
                    // Die letzte Message enthält nur den Delimiter bzw. die unvollständige Nachricht
                    for (int i = 0; i < messages.Length - 1; i++)
                    {
                        // Abschließendes \r und \n entfernen
                        MessageReceived(this, messages[i].TrimEnd(new char[] { '\r', '\n' }));
                    }
                }

                // Den Empfang der nächsten Daten anstoßen
                try
                {
                    client.BeginReceive(state.buffer, 0, ReceiveStateObject.BUFFER_SIZE, 0,
                        new AsyncCallback(receiveCallback), state);
                }
                // Disconnect an CallbackHandler weiterreichen, falls die
                // Verbindung zum Client getrennt wurde.
                catch (System.ObjectDisposedException)
                {
                    disconnect();
                    return;
                }
            }
            catch (Exception e)
            {
                handleException(e);
            }
        }

        /// <summary>
        /// Fragt ab oder legt fest, ob versucht werden soll, die Verbindung nach einem Abbruch 
        /// automatisch wiederherzustellen.
        /// </summary>
        public bool AutoReconnect
        {
            get
            {
                return this._autoReconnect;
            }
            set
            {
                this._autoReconnect = value;
            }
        }

        #endregion

        /// <summary>
        /// Wird aufgerufen, um periodisch eine Verbindung wiederaufzubauen, wenn diese abgebrochen wurde
        /// und der AutoReconnect aktiviert ist.
        /// </summary>
        /// <param name="state"></param>
        private static void autoReconnectTimerCallback(Object state)
        {
            TcpClientAsynchronous instance = (TcpClientAsynchronous)state;

            // Ist AutoReconnect deaktiviert, muss nichts unternommen werden
            if (!instance.AutoReconnect)
                return;

            // Ist die Verbindung nicht unterbrochen, muss ebenfalls nichts unternommen werden
            if (instance.Connected)
                return;

            // Wurde explizit ein disconnect aufgerufen, so soll AutoReconnect nicht zum Zuge kommen
            if (!instance._tryAutoReconnect)
                return;

            instance.connect();
        }
    }
}
