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
using System.Net;
using System.Net.Sockets;

namespace de.christianleberfinger.dotnet.pocketknife.Net
{
    /// <summary>
    /// Broadcaster is able to broadcast (multi cast) datagrams to one ore more machines in the same subnet.
    /// </summary>
    public class UDPBroadcaster
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private IPEndPoint _ipEndpoint;

        /// <summary>
        /// Creates a new instance of Broadcaster, that is able to broadcast datagrams to all machines in the same subnet.
        /// Therefore Broadcaster tries to create a broadcast address by replacing the last part of this machine's IP address
        /// with 0xFF. If you do have a subnet with a different address range you can use one of the alternative
        /// constructors. Unfortunately there may be cases when IPAddress.Broadcast will not work.
        /// </summary>
        /// <param name="port">The port you want to communicate on.</param>
        public UDPBroadcaster(int port)
        {
            // try to create broadcast address for the current subnet
            // e.g. 192.168.1.255 (das letzte byte auf 0xff)
            byte[] broadcastAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].GetAddressBytes();
            broadcastAddress[3] = 255;
            
            // IP-Endpoint initialisieren
            _ipEndpoint = new IPEndPoint(new IPAddress(broadcastAddress), port);
        }

        /// <summary>
        /// Creates a new instance of Broadcaster, that is able to broadcast datagrams to all machines in the same subnet.
        /// </summary>
        /// <param name="port">The port you want to communicate on.</param>
        /// <param name="broadcastAddress">Your broadcast address. Note: There may be cases when IPAddress.Broadcast will not work.</param>
        public UDPBroadcaster(int port, byte[] broadcastAddress)
        {
            // IP-Endpoint initialisieren
            _ipEndpoint = new IPEndPoint(new IPAddress(broadcastAddress), port);
        }

        /// <summary>
        /// Creates a new instance of Broadcaster, that is able to broadcast datagrams to all machines in the same subnet.
        /// </summary>
        /// <param name="port">The port you want to communicate on.</param>
        /// <param name="broadcastAddress">Your broadcast address. Note: There may be cases when IPAddress.Broadcast will not work.</param>
        public UDPBroadcaster(int port, string broadcastAddress)
        {
            // IP-Endpoint initialisieren
            _ipEndpoint = new IPEndPoint(IPAddress.Parse(broadcastAddress), port);
        }

        /// <summary>
        /// Creates a new instance of Broadcaster, that is able to broadcast datagrams to all machines in the same subnet.
        /// </summary>
        /// <param name="port">The port you want to communicate on.</param>
        /// <param name="broadcastAddress">Your broadcast address. Note: There may be cases when IPAddress.Broadcast will not work.</param>
        public UDPBroadcaster(int port, IPAddress broadcastAddress)
        {
            _ipEndpoint = new IPEndPoint(broadcastAddress, port);
        }

        /// <summary>
        /// Broadcasts the given datagram to all machines in the same subnet.
        /// </summary>
        /// <param name="datagram">The datagram you want to broadcast.</param>
        public void send(byte[] datagram)
        {
            _socket.SendTo(datagram, _ipEndpoint);
        }
    }
}
