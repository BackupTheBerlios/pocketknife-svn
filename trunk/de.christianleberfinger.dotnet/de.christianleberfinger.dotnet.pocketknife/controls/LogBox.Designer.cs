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
namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    partial class LogBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._listBox = new System.Windows.Forms.ListBox();
            this._btSave = new System.Windows.Forms.Button();
            this._btCopyClipboard = new System.Windows.Forms.Button();
            this._btClear = new System.Windows.Forms.Button();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._lblHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _listBox
            // 
            this._listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._listBox.FormattingEnabled = true;
            this._listBox.IntegralHeight = false;
            this._listBox.Location = new System.Drawing.Point(-1, 25);
            this._listBox.Name = "_listBox";
            this._listBox.ScrollAlwaysVisible = true;
            this._listBox.Size = new System.Drawing.Size(274, 95);
            this._listBox.TabIndex = 3;
            // 
            // _btSave
            // 
            this._btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btSave.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this._btSave.Image = global::de.christianleberfinger.dotnet.pocketknife.Properties.Resources.media_floppy;
            this._btSave.Location = new System.Drawing.Point(183, 1);
            this._btSave.Name = "_btSave";
            this._btSave.Size = new System.Drawing.Size(25, 25);
            this._btSave.TabIndex = 0;
            this._btSave.UseVisualStyleBackColor = true;
            this._btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // _btCopyClipboard
            // 
            this._btCopyClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btCopyClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btCopyClipboard.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this._btCopyClipboard.Image = global::de.christianleberfinger.dotnet.pocketknife.Properties.Resources.edit_copy;
            this._btCopyClipboard.Location = new System.Drawing.Point(214, 1);
            this._btCopyClipboard.Name = "_btCopyClipboard";
            this._btCopyClipboard.Size = new System.Drawing.Size(25, 25);
            this._btCopyClipboard.TabIndex = 1;
            this._btCopyClipboard.UseVisualStyleBackColor = true;
            this._btCopyClipboard.Click += new System.EventHandler(this.btCopyClipboard_Click);
            // 
            // _btClear
            // 
            this._btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btClear.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this._btClear.Image = global::de.christianleberfinger.dotnet.pocketknife.Properties.Resources.edit_clear;
            this._btClear.Location = new System.Drawing.Point(245, 1);
            this._btClear.Name = "_btClear";
            this._btClear.Size = new System.Drawing.Size(25, 25);
            this._btClear.TabIndex = 2;
            this._btClear.UseVisualStyleBackColor = true;
            this._btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // _lblHeader
            // 
            this._lblHeader.AutoSize = true;
            this._lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblHeader.Location = new System.Drawing.Point(2, 6);
            this._lblHeader.Name = "_lblHeader";
            this._lblHeader.Size = new System.Drawing.Size(56, 13);
            this._lblHeader.TabIndex = 4;
            this._lblHeader.Text = "Logbook";
            // 
            // LogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._lblHeader);
            this.Controls.Add(this._listBox);
            this.Controls.Add(this._btSave);
            this.Controls.Add(this._btCopyClipboard);
            this.Controls.Add(this._btClear);
            this.Name = "LogBox";
            this.Size = new System.Drawing.Size(273, 121);
            this.Load += new System.EventHandler(this.LogBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _listBox;
        private System.Windows.Forms.Button _btClear;
        private System.Windows.Forms.Button _btCopyClipboard;
        private System.Windows.Forms.Button _btSave;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
        private System.Windows.Forms.Label _lblHeader;
    }
}
