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

namespace de.christianleberfinger.dotnet.controls
{
    public partial class LogBox : UserControl
    {
        bool autoSelectLastEntry = true;
        int maxEntryCount = 500;
        bool clearButton = true;

        delegate void StringHandler(string s);
        delegate void StringArrayHandler(string[] strings);
        delegate void VoidHandler();

        public LogBox()
        {
            InitializeComponent();
            this.Resize += new EventHandler(LogBox_Resize);
            updateClearButton();

            initToolTips();
        }

        /// <summary>
        /// Initializes the tooltips for the subcomponents of this logbox.
        /// </summary>
        protected void initToolTips()
        {
            toolTip1.SetToolTip(btSave, "Save contents of logbook to file.");
            toolTip1.SetToolTip(btClear, "Clear contents of logbook.");
            toolTip1.SetToolTip(btCopyClipboard, "Copy contents of logbook to clipboard.");
        }

        void LogBox_Resize(object sender, EventArgs e)
        {
            updateClearButton();
        }

        /// <summary>
        /// Sets or gets a value that indicates whether the last most recent entry of LogBox is automatically selected when getting added.
        /// </summary>
        [Description("Sets or gets a value that indicates whether the last most recent entry of LogBox is automatically selected when getting added."),
         CategoryAttribute("LogBox")]
        public bool AutoSelectLastEntry
        {
            get { return autoSelectLastEntry; }
            set
            {
                autoSelectLastEntry = value;
                if (value)
                    selectLastEntry();
            }
        }

        /// <summary>
        /// Sets or gets a value that indicates whether a clear-button is visible that allows the user to delete the entries of the LogBox.
        /// </summary>
        [Description("Sets or gets a value that indicates whether a clear-button is visible that allows the user to delete the entries of the LogBox."),
         CategoryAttribute("LogBox")]
        public bool ClearButton
        {
            get { return clearButton; }
            set
            {
                clearButton = value;
                updateClearButton();
            }
        }

        /// <summary>
        /// Sets or gets the maximum number of entries that are displayed in the LogBox.
        /// </summary>
        [Description("Sets or gets the maximum number of entries that are displayed in the LogBox."),
         CategoryAttribute("LogBox")]
        public int MaxEntryCount
        {
            get { return maxEntryCount; }
            set { maxEntryCount = value; }
        }

        /// <summary>
        /// Adds a new message to this LogBox. You can call this method without using Invoke().
        /// </summary>
        /// <param name="message"></param>
        public void log(string message)
        {
            if (InvokeRequired)
                this.Invoke(new StringHandler(log), new object[] { message });
            else
            {
                if (listBox1.Items.Count > maxEntryCount)
                    listBox1.Items.RemoveAt(0);

                listBox1.Items.Add(message);
                selectLastEntry();
            }
        }

        /// <summary>
        /// Adds a new message to this LogBox. You can call this method without using Invoke().
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void log(string format, params object[] args)
        {
            log(string.Format(format, args));
        }

        /// <summary>
        /// Adds new messages to this LogBox. You can call this method without using Invoke().
        /// </summary>
        /// <param name="messages"></param>
        public void log(string[] messages)
        {
            if (InvokeRequired)
                this.Invoke(new StringArrayHandler(log), new object[] { messages });
            else
            {
                listBox1.BeginUpdate();
                listBox1.Items.AddRange(messages);
                selectLastEntry();
                listBox1.EndUpdate();
            }
        }

        /// <summary>
        /// Removes all entries of this LogBox. You can call this method without using Invoke().
        /// </summary>
        public void clear()
        {
            this.Invoke(new VoidHandler(listBox1.Items.Clear));
        }

        protected void selectLastEntry()
        {
            if (listBox1.InvokeRequired)
                listBox1.Invoke(new VoidHandler(selectLastEntry));
            else
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        void updateClearButton()
        {
            if (btClear.InvokeRequired)
                btClear.Invoke(new VoidHandler(updateClearButton));
            else
            {
                //btClear.Location = new Point(this.Width - (btClear.Width + 2), -2);
                btClear.Visible = clearButton;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btCopyClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object o in listBox1.Items)
            {
                sb.Append(o.ToString());
                sb.AppendLine();
            }
            Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
        }

        private void LogBox_Load(object sender, EventArgs e)
        {

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveContentToFile(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Copies the entries of this LogBox to an array of strings.
        /// </summary>
        /// <returns>Log entries as strings.</returns>
        public string[] toArray()
        {
            List<string> content = new List<string>();
            foreach (object o in listBox1.Items)
            {
                content.Add(o.ToString());
            }
            return content.ToArray();
        }

        /// <summary>
        /// Saves the entries of this LogBox in a text file.
        /// </summary>
        /// <param name="file"></param>
        public void saveContentToFile(string file)
        {
            System.IO.File.WriteAllLines(file, this.toArray());
        }

    }
}
