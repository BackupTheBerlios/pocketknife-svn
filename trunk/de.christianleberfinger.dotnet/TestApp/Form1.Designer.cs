namespace TestApp
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.serialPortControl1 = new de.christianleberfinger.dotnet.controls.SerialPortControl();
            this.loggingBox1 = new de.christianleberfinger.dotnet.controls.LogBox();
            this.serialPort1 = new de.christianleberfinger.dotnet.IO.SerialPort();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 139);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // serialPortControl1
            // 
            this.serialPortControl1.Location = new System.Drawing.Point(196, 155);
            this.serialPortControl1.Name = "serialPortControl1";
            this.serialPortControl1.Size = new System.Drawing.Size(324, 150);
            this.serialPortControl1.TabIndex = 3;
            // 
            // loggingBox1
            // 
            this.loggingBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.loggingBox1.AutoSelectLastEntry = true;
            this.loggingBox1.BackColor = System.Drawing.SystemColors.Control;
            this.loggingBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loggingBox1.ClearButton = true;
            this.loggingBox1.CopyButton = true;
            this.loggingBox1.Location = new System.Drawing.Point(12, 12);
            this.loggingBox1.MaxEntryCount = 500;
            this.loggingBox1.Name = "loggingBox1";
            this.loggingBox1.SaveButtonVisible = true;
            this.loggingBox1.Size = new System.Drawing.Size(430, 121);
            this.loggingBox1.TabIndex = 0;
            // 
            // serialPort1
            // 
            this.serialPort1.OnByteReceived += new de.christianleberfinger.dotnet.IO.SerialPort.ByteReceivedHandler(this.serialPort1_OnByteReceived_1);
            this.serialPort1.OnConnectionStateChange += new de.christianleberfinger.dotnet.IO.SerialPort.ConnectionStateChangedHandler(this.serialPort1_OnConnectionStateChange);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 264);
            this.Controls.Add(this.serialPortControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loggingBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private de.christianleberfinger.dotnet.controls.LogBox loggingBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private de.christianleberfinger.dotnet.IO.SerialPort serialPort1;
        private de.christianleberfinger.dotnet.controls.SerialPortControl serialPortControl1;
    }
}

