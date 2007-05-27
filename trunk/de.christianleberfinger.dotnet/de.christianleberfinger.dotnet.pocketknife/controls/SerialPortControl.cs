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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using de.christianleberfinger.dotnet.IO;

namespace de.christianleberfinger.dotnet.controls
{
    /// <summary>
    /// Offers a simple GUI control for an event based serial port.
    /// </summary>
    public partial class SerialPortControl : UserControl
    {
        SerialPort port = new SerialPort();

        /// <summary>
        /// Returns the inner serial port class that handles the I/O communication.
        /// </summary>
        public SerialPort Port
        {
            get { return port; }
        }

        /// <summary>
        /// Occurs every time when a byte was received from the serial port.
        /// </summary>
        public event SerialPort.ByteReceivedHandler OnByteReceived
        {
            add
            {
                port.OnByteReceived += value;
            }
            remove
            {
                port.OnByteReceived -= value;
            }
        }

        /// <summary>
        /// Creates a new instance of SerialPortControl
        /// </summary>
        public SerialPortControl()
        {
            InitializeComponent();
            initPortNames();
            port.OnConnectionStateChange += new SerialPort.ConnectionStateChangedHandler(port_OnConnectionStateChange);
        }

        void port_OnConnectionStateChange(bool connected)
        {
            updateConnectButton();
        }

        private void SerialPortControl_Load(object sender, EventArgs e)
        {

        }

        delegate void VoidHandler();
        delegate void StringHandler(string s);

        private void initPortNames()
        {
            if (cbPortName.InvokeRequired)
                cbPortName.Invoke(new VoidHandler(initPortNames));
            else
                cbPortName.Items.AddRange(SerialPort.GetPortNames());
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
                port.Close();
            else
                open();

            updateConnectButton();
        }

        /// <summary>
        /// Opens the serialport that was specified in the combobox and starts an internal
        /// thread that reads from it. You can subscribe the event 'OnByteReceived' to
        /// be notificated about received data.
        /// </summary>
        private void open()
        {
            port.PortName = cbPortName.Text;
            port.Open();
        }

        private void updateConnectButton()
        {
            if (btConnect.InvokeRequired)
            {
                btConnect.Invoke(new VoidHandler(updateConnectButton));
            }
            else
            {
                if (port.IsOpen)
                    btConnect.Text = "Disconnect";
                else
                    btConnect.Text = "Connect";
            }
        }
    }
}
