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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.cbMuted = new System.Windows.Forms.Label();
            this.rbtStop = new System.Windows.Forms.RadioButton();
            this.rbtPause = new System.Windows.Forms.RadioButton();
            this.rbtPlay = new System.Windows.Forms.RadioButton();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.timeProgressBar1 = new de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Enabled = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 51);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(298, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 17);
            this.lblStatus.Text = "No media";
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(232, 17);
            this.lblTime.Spring = true;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // cbMuted
            // 
            this.cbMuted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMuted.Image = global::de.christianleberfinger.dotnet.pocketknife.Properties.Resources.audio_volume_medium;
            this.cbMuted.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbMuted.Location = new System.Drawing.Point(209, 20);
            this.cbMuted.Name = "cbMuted";
            this.cbMuted.Size = new System.Drawing.Size(32, 23);
            this.cbMuted.TabIndex = 6;
            this.cbMuted.Click += new System.EventHandler(this.cbMuted_Click);
            this.cbMuted.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cbMuted_MouseUp);
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
            // tbVolume
            // 
            this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVolume.AutoSize = false;
            this.tbVolume.Location = new System.Drawing.Point(233, 18);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(62, 29);
            this.tbVolume.SmallChange = 10;
            this.tbVolume.TabIndex = 7;
            this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbVolume.Value = 100;
            this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);
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
            this.Controls.Add(this.cbMuted);
            this.Controls.Add(this.tbVolume);
            this.Controls.Add(this.rbtStop);
            this.Controls.Add(this.rbtPause);
            this.Controls.Add(this.rbtPlay);
            this.Controls.Add(this.timeProgressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MediaControl";
            this.Size = new System.Drawing.Size(298, 73);
            this.Load += new System.EventHandler(this.MediaControl_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Timer updateTimer;
        private de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar timeProgressBar1;
        private System.Windows.Forms.RadioButton rbtPlay;
        private System.Windows.Forms.RadioButton rbtPause;
        private System.Windows.Forms.RadioButton rbtStop;
        private System.Windows.Forms.Label cbMuted;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.TrackBar tbVolume;
    }
}
