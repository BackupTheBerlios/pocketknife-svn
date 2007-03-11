using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

    }
}