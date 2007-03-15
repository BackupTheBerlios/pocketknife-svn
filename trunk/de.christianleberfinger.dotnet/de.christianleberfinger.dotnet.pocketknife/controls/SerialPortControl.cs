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
    public partial class SerialPortControl : UserControl
    {
        SerialPort port = new SerialPort();

        public SerialPort Port
        {
            get { return port; }
        }

        public SerialPortControl()
        {
            InitializeComponent();
            initPortNames();
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
            port.Open();

            updateConnectButton();
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
