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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using de.christianleberfinger.dotnet.pocketknife.controls;
using de.christianleberfinger.dotnet.pocketknife;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            propertyGridConfig.SelectedObject = Config.Settings;
        }

        public const string CALC_WINDOW_CLASS = "SciCalc";
        public const string CALC_WINDOW_TITLE = null; // the title of the window doesn't matter

        ExternalWindow calculatorWindow = new ExternalWindow(CALC_WINDOW_CLASS, CALC_WINDOW_TITLE);

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
                loggingBox1.log(i.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void serialPort1_OnByteReceived_1(byte receivedByte)
        {
            loggingBox1.log("Message received: " + receivedByte.ToString());
        }

        private void serialPort1_OnConnectionStateChange(bool connected)
        {
            loggingBox1.log("Connected: {0}", connected);
        }

        private void serialPortControl1_OnByteReceived(byte receivedByte)
        {
            loggingBox1.log("0x{0}", receivedByte.ToString("X"));
        }

        private void serialPortControl1_Load(object sender, EventArgs e)
        {

        }

        private void btCalcOne_Click(object sender, EventArgs e)
        {
            calculatorWindow.sendKey( "1" );
        }

        private void btCalcTwo_Click(object sender, EventArgs e)
        {
            calculatorWindow.sendKey( "2" );
        }

        private void btCalcThree_Click(object sender, EventArgs e)
        {
            calculatorWindow.sendKey( "3" );
        }

        private void btMask_Click(object sender, EventArgs e)
        {
            long l = long.Parse(tbBitValue.Text);
            bool b = BitUtil.getBit(l, (byte)tbBitIndex.Value);
            tbBitResult.Text = b ? "1" : "0";
        }

    }
}