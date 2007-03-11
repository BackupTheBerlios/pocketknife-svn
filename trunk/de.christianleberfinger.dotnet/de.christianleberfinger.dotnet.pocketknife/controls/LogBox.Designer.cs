namespace de.christianleberfinger.dotnet.controls
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btCopyClipboard = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(275, 108);
            this.listBox1.TabIndex = 0;
            // 
            // btCopyClipboard
            // 
            this.btCopyClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCopyClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCopyClipboard.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btCopyClipboard.Image = global::de.christianleberfinger.dotnet.Properties.Resources.edit_copy;
            this.btCopyClipboard.Location = new System.Drawing.Point(204, 0);
            this.btCopyClipboard.Name = "btCopyClipboard";
            this.btCopyClipboard.Size = new System.Drawing.Size(25, 25);
            this.btCopyClipboard.TabIndex = 2;
            this.btCopyClipboard.UseVisualStyleBackColor = true;
            this.btCopyClipboard.Click += new System.EventHandler(this.btCopyClipboard_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClear.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btClear.Image = global::de.christianleberfinger.dotnet.Properties.Resources.edit_clear;
            this.btClear.Location = new System.Drawing.Point(230, 0);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(25, 25);
            this.btClear.TabIndex = 1;
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // LogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btCopyClipboard);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.listBox1);
            this.Name = "LogBox";
            this.Size = new System.Drawing.Size(275, 108);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btCopyClipboard;
    }
}
