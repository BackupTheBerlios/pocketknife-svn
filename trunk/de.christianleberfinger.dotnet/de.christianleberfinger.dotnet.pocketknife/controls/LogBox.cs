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

namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    /// <summary>
    /// LogBox adds some extra functionality to the ListBox control of the .NET-framework
    /// that makes it an ideal component for the purpose of logging.
    /// Problems with cross-threaded access are avoided internally. That means, you only
    /// have to drag&amp;drop LogBox into your GUI and can use it with just calling the log() method.
    /// </summary>
    public partial class LogBox : UserControl
    {
        bool _autoSelectLastEntry = true;
        int _maxEntryCount = 500;
        bool _clearButtonVisible = true;
        bool _copyButtonVisible = true;
        bool _saveButtonVisible = true;

        delegate void StringHandler(string s);
        delegate void StringArrayHandler(string[] strings);
        delegate void VoidHandler();

        /// <summary>
        /// Creates a new LogBox.
        /// </summary>
        public LogBox()
        {
            InitializeComponent();
            this.Resize += new EventHandler(LogBox_Resize);

            updateButtons();
            initToolTips();
        }

        /// <summary>
        /// Initializes the tooltips for the subcomponents of this logbox.
        /// </summary>
        protected void initToolTips()
        {
            _toolTip.SetToolTip(_btSave, "Save contents of logbook to file.");
            _toolTip.SetToolTip(_btClear, "Clear contents of logbook.");
            _toolTip.SetToolTip(_btCopyClipboard, "Copy contents of logbook to clipboard.");
        }

        void LogBox_Resize(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Sets or gets a value that indicates whether the last most recent entry of LogBox is automatically selected when getting added.
        /// </summary>
        [Description("Sets or gets a value that indicates whether the last most recent entry of LogBox is automatically selected when getting added."),
         CategoryAttribute("LogBox")]
        public bool AutoSelectLastEntry
        {
            get { return _autoSelectLastEntry; }
            set
            {
                _autoSelectLastEntry = value;
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
            get { return _clearButtonVisible; }
            set
            {
                _clearButtonVisible = value;
                updateButtons();
            }
        }

        /// <summary>
        /// Gets ot sets a boolean value indicating whether the save button is visible.
        /// </summary>
        public bool SaveButtonVisible
        {
            get { return _saveButtonVisible; }
            set
            {
                _saveButtonVisible = value;
                updateButtons();
            }
        }

        /// <summary>
        /// Sets or gets a value that indicates whether a button is visible that allows the user to copy the entries of the LogBox to the clipboard.
        /// </summary>
        [Description("Sets or gets a value that indicates whether a button is visible that allows the user to copy the entries of the LogBox to the clipboard."),
         CategoryAttribute("LogBox")]
        public bool CopyButton
        {
            get { return _copyButtonVisible; }
            set
            {
                _copyButtonVisible = value;
                updateButtons();
            }
        }

        /// <summary>
        /// Sets or gets the maximum number of entries that are displayed in the LogBox.
        /// </summary>
        [Description("Sets or gets the maximum number of entries that are displayed in the LogBox."),
         CategoryAttribute("LogBox")]
        public int MaxEntryCount
        {
            get { return _maxEntryCount; }
            set { _maxEntryCount = value; }
        }

        /// <summary>
        /// Adds a new message to this LogBox. You can call this method without using Invoke().
        /// </summary>
        /// <param name="message">The message you want to log.</param>
        public void log(string message)
        {
            if (InvokeRequired)
                this.Invoke(new StringHandler(log), new object[] { message });
            else
            {
                if (_listBox.Items.Count > _maxEntryCount)
                    _listBox.Items.RemoveAt(0);

                _listBox.Items.Add(message);
                selectLastEntry();
            }
        }

        /// <summary>
        /// Adds a new message to this LogBox. You can call this method without using Invoke().
        /// </summary>
        /// <param name="format">A string that can contain zero or more format items, like e.g. {0}.</param>
        /// <param name="args">An array of objects that will be used to fill the format item's gaps.</param>
        public void log(string format, params object[] args)
        {
            log(string.Format(format, args));
        }

        /// <summary>
        /// Adds new messages to this LogBox. You can call this method without using Invoke().
        /// </summary>
        /// <param name="messages">The messages you want to log.</param>
        public void log(string[] messages)
        {
            if (InvokeRequired)
                this.Invoke(new StringArrayHandler(log), new object[] { messages });
            else
            {
                _listBox.BeginUpdate();
                _listBox.Items.AddRange(messages);
                selectLastEntry();
                _listBox.EndUpdate();
            }
        }

        /// <summary>
        /// Removes all entries of this LogBox. You can call this method without using Invoke().
        /// </summary>
        public void clear()
        {
            this.Invoke(new VoidHandler(_listBox.Items.Clear));
        }

        /// <summary>
        /// Selects the last entry
        /// </summary>
        protected void selectLastEntry()
        {
            if (_listBox.InvokeRequired)
                _listBox.Invoke(new VoidHandler(selectLastEntry));
            else
                _listBox.SelectedIndex = _listBox.Items.Count - 1;
        }

        void updateButtons()
        {
            if (this.InvokeRequired)
                this.Invoke(new VoidHandler(updateButtons));
            else
            {
                _btClear.Visible = _clearButtonVisible;
                _btCopyClipboard.Visible = _copyButtonVisible;
                _btSave.Visible = _saveButtonVisible;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btCopyClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object o in _listBox.Items)
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
            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveContentToFile(_saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Copies the entries of this LogBox to an array of strings.
        /// </summary>
        /// <returns>Log entries as strings.</returns>
        public string[] toArray()
        {
            List<string> content = new List<string>();
            foreach (object o in _listBox.Items)
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

        /// <summary>
        /// Sets the title of the LogBox.
        /// </summary>
        public string Title
        {
            get { return _lblHeader.Text; }
            set {
                setHeader(value);            
            }
        }

        void setHeader(string title)
        {
            if (_lblHeader.InvokeRequired)
            {
                _lblHeader.Invoke(new StringHandler(setHeader), title);
            }
            else
            _lblHeader.Text = title;
        }
        
    }
}
