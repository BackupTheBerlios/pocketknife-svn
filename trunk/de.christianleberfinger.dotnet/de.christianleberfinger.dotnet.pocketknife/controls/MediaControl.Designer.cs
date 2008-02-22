/*
 * 
 * Copyright (c) 2008 Christian Leberfinger
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
    partial class MediaControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaControl));
            this.lblTime = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.rbtPlay = new System.Windows.Forms.RadioButton();
            this.rbtPause = new System.Windows.Forms.RadioButton();
            this.rbtStop = new System.Windows.Forms.RadioButton();
            this.timeProgressBar1 = new de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Location = new System.Drawing.Point(3, 24);
            this.lblTime.Name = "lblTime";
            this.lblTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTime.Size = new System.Drawing.Size(292, 13);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "0:00 / 7:53";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Enabled = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 51);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(298, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 17);
            this.lblStatus.Text = "No media";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // rbtPlay
            // 
            this.rbtPlay.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtPlay.Image = ((System.Drawing.Image)(resources.GetObject("rbtPlay.Image")));
            this.rbtPlay.Location = new System.Drawing.Point(3, 18);
            this.rbtPlay.Name = "rbtPlay";
            this.rbtPlay.Size = new System.Drawing.Size(37, 25);
            this.rbtPlay.TabIndex = 0;
            this.rbtPlay.UseVisualStyleBackColor = true;
            this.rbtPlay.CheckedChanged += new System.EventHandler(this.rbtPlay_CheckedChanged);
            // 
            // rbtPause
            // 
            this.rbtPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtPause.Image = ((System.Drawing.Image)(resources.GetObject("rbtPause.Image")));
            this.rbtPause.Location = new System.Drawing.Point(46, 18);
            this.rbtPause.Name = "rbtPause";
            this.rbtPause.Size = new System.Drawing.Size(37, 25);
            this.rbtPause.TabIndex = 1;
            this.rbtPause.UseVisualStyleBackColor = true;
            this.rbtPause.CheckedChanged += new System.EventHandler(this.rbtPause_CheckedChanged);
            // 
            // rbtStop
            // 
            this.rbtStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtStop.Checked = true;
            this.rbtStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtStop.Image = ((System.Drawing.Image)(resources.GetObject("rbtStop.Image")));
            this.rbtStop.Location = new System.Drawing.Point(89, 18);
            this.rbtStop.Name = "rbtStop";
            this.rbtStop.Size = new System.Drawing.Size(37, 25);
            this.rbtStop.TabIndex = 2;
            this.rbtStop.TabStop = true;
            this.rbtStop.UseVisualStyleBackColor = true;
            this.rbtStop.CheckedChanged += new System.EventHandler(this.rbtStop_CheckedChanged);
            // 
            // timeProgressBar1
            // 
            this.timeProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timeProgressBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeProgressBar1.Color = System.Drawing.Color.MidnightBlue;
            this.timeProgressBar1.ColorWhileDragging = System.Drawing.Color.LightSteelBlue;
            this.timeProgressBar1.Location = new System.Drawing.Point(3, 3);
            this.timeProgressBar1.Name = "timeProgressBar1";
            this.timeProgressBar1.RelativePosition = 0;
            this.timeProgressBar1.Size = new System.Drawing.Size(292, 10);
            this.timeProgressBar1.TabIndex = 5;
            this.timeProgressBar1.OnValueChanged += new de.christianleberfinger.dotnet.pocketknife.GenericEventHandler<de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar, de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar.ValueUpdatedEventArgs>(this.timeProgressBar1_OnValueChanged);
            // 
            // MediaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rbtStop);
            this.Controls.Add(this.rbtPause);
            this.Controls.Add(this.rbtPlay);
            this.Controls.Add(this.timeProgressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblTime);
            this.Name = "MediaControl";
            this.Size = new System.Drawing.Size(298, 73);
            this.Load += new System.EventHandler(this.MediaControl_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Timer updateTimer;
        private de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar timeProgressBar1;
        private System.Windows.Forms.RadioButton rbtPlay;
        private System.Windows.Forms.RadioButton rbtPause;
        private System.Windows.Forms.RadioButton rbtStop;
    }
}
