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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageLogging = new System.Windows.Forms.TabPage();
            this.tabPageSerialPort = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageConfiguration = new System.Windows.Forms.TabPage();
            this.propertyGridConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCalcThree = new System.Windows.Forms.Button();
            this.btCalcTwo = new System.Windows.Forms.Button();
            this.btCalcOne = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbBitIndex = new System.Windows.Forms.NumericUpDown();
            this.tbBitMaskComplete = new System.Windows.Forms.TextBox();
            this.tbBitResult = new System.Windows.Forms.TextBox();
            this.tbBitValue = new System.Windows.Forms.TextBox();
            this.btMask = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btCountdownStart = new System.Windows.Forms.Button();
            this.tbCountdownTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCountdownInfo = new System.Windows.Forms.Label();
            this.loggingBox1 = new de.christianleberfinger.dotnet.pocketknife.controls.LogBox();
            this.serialPortControl1 = new de.christianleberfinger.dotnet.pocketknife.controls.SerialPortControl();
            this.serialPort1 = new de.christianleberfinger.dotnet.pocketknife.IO.SerialPort();
            this.btCountdownCancel = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageLogging.SuspendLayout();
            this.tabPageSerialPort.SuspendLayout();
            this.tabPageConfiguration.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBitIndex)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageLogging);
            this.tabControlMain.Controls.Add(this.tabPageSerialPort);
            this.tabControlMain.Controls.Add(this.tabPageConfiguration);
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Controls.Add(this.tabPage3);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(466, 298);
            this.tabControlMain.TabIndex = 4;
            // 
            // tabPageLogging
            // 
            this.tabPageLogging.Controls.Add(this.loggingBox1);
            this.tabPageLogging.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogging.Name = "tabPageLogging";
            this.tabPageLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogging.Size = new System.Drawing.Size(458, 272);
            this.tabPageLogging.TabIndex = 0;
            this.tabPageLogging.Text = "Logging";
            this.tabPageLogging.UseVisualStyleBackColor = true;
            // 
            // tabPageSerialPort
            // 
            this.tabPageSerialPort.Controls.Add(this.label2);
            this.tabPageSerialPort.Controls.Add(this.label1);
            this.tabPageSerialPort.Controls.Add(this.button2);
            this.tabPageSerialPort.Controls.Add(this.button1);
            this.tabPageSerialPort.Controls.Add(this.serialPortControl1);
            this.tabPageSerialPort.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerialPort.Name = "tabPageSerialPort";
            this.tabPageSerialPort.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerialPort.Size = new System.Drawing.Size(458, 272);
            this.tabPageSerialPort.TabIndex = 1;
            this.tabPageSerialPort.Text = "Serial Port";
            this.tabPageSerialPort.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SerialPortControl:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "SerialPort class:";
            // 
            // tabPageConfiguration
            // 
            this.tabPageConfiguration.Controls.Add(this.propertyGridConfig);
            this.tabPageConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguration.Name = "tabPageConfiguration";
            this.tabPageConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguration.Size = new System.Drawing.Size(458, 272);
            this.tabPageConfiguration.TabIndex = 2;
            this.tabPageConfiguration.Text = "Configuration";
            this.tabPageConfiguration.UseVisualStyleBackColor = true;
            // 
            // propertyGridConfig
            // 
            this.propertyGridConfig.Location = new System.Drawing.Point(6, 6);
            this.propertyGridConfig.Name = "propertyGridConfig";
            this.propertyGridConfig.Size = new System.Drawing.Size(446, 180);
            this.propertyGridConfig.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 272);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "External Window";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btCalcThree);
            this.groupBox1.Controls.Add(this.btCalcTwo);
            this.groupBox1.Controls.Add(this.btCalcOne);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculator";
            // 
            // btCalcThree
            // 
            this.btCalcThree.Location = new System.Drawing.Point(100, 19);
            this.btCalcThree.Name = "btCalcThree";
            this.btCalcThree.Size = new System.Drawing.Size(41, 23);
            this.btCalcThree.TabIndex = 0;
            this.btCalcThree.Text = "3";
            this.btCalcThree.UseVisualStyleBackColor = true;
            this.btCalcThree.Click += new System.EventHandler(this.btCalcThree_Click);
            // 
            // btCalcTwo
            // 
            this.btCalcTwo.Location = new System.Drawing.Point(53, 19);
            this.btCalcTwo.Name = "btCalcTwo";
            this.btCalcTwo.Size = new System.Drawing.Size(41, 23);
            this.btCalcTwo.TabIndex = 0;
            this.btCalcTwo.Text = "2";
            this.btCalcTwo.UseVisualStyleBackColor = true;
            this.btCalcTwo.Click += new System.EventHandler(this.btCalcTwo_Click);
            // 
            // btCalcOne
            // 
            this.btCalcOne.Location = new System.Drawing.Point(6, 19);
            this.btCalcOne.Name = "btCalcOne";
            this.btCalcOne.Size = new System.Drawing.Size(41, 23);
            this.btCalcOne.TabIndex = 0;
            this.btCalcOne.Text = "1";
            this.btCalcOne.UseVisualStyleBackColor = true;
            this.btCalcOne.Click += new System.EventHandler(this.btCalcOne_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbBitIndex);
            this.tabPage2.Controls.Add(this.tbBitMaskComplete);
            this.tabPage2.Controls.Add(this.tbBitResult);
            this.tabPage2.Controls.Add(this.tbBitValue);
            this.tabPage2.Controls.Add(this.btMask);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(458, 272);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "BitMasking";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbBitIndex
            // 
            this.tbBitIndex.Location = new System.Drawing.Point(112, 8);
            this.tbBitIndex.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.tbBitIndex.Name = "tbBitIndex";
            this.tbBitIndex.Size = new System.Drawing.Size(92, 20);
            this.tbBitIndex.TabIndex = 3;
            // 
            // tbBitMaskComplete
            // 
            this.tbBitMaskComplete.Location = new System.Drawing.Point(6, 34);
            this.tbBitMaskComplete.Name = "tbBitMaskComplete";
            this.tbBitMaskComplete.ReadOnly = true;
            this.tbBitMaskComplete.Size = new System.Drawing.Size(198, 20);
            this.tbBitMaskComplete.TabIndex = 2;
            // 
            // tbBitResult
            // 
            this.tbBitResult.Location = new System.Drawing.Point(291, 8);
            this.tbBitResult.Name = "tbBitResult";
            this.tbBitResult.ReadOnly = true;
            this.tbBitResult.Size = new System.Drawing.Size(161, 20);
            this.tbBitResult.TabIndex = 2;
            // 
            // tbBitValue
            // 
            this.tbBitValue.Location = new System.Drawing.Point(6, 8);
            this.tbBitValue.Name = "tbBitValue";
            this.tbBitValue.Size = new System.Drawing.Size(100, 20);
            this.tbBitValue.TabIndex = 1;
            this.tbBitValue.Text = "170";
            // 
            // btMask
            // 
            this.btMask.Location = new System.Drawing.Point(210, 6);
            this.btMask.Name = "btMask";
            this.btMask.Size = new System.Drawing.Size(75, 23);
            this.btMask.TabIndex = 0;
            this.btMask.Text = "Mask";
            this.btMask.UseVisualStyleBackColor = true;
            this.btMask.Click += new System.EventHandler(this.btMask_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btCountdownCancel);
            this.tabPage3.Controls.Add(this.lblCountdownInfo);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.tbCountdownTime);
            this.tabPage3.Controls.Add(this.btCountdownStart);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(458, 272);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "tabPage3";
            // 
            // btCountdownStart
            // 
            this.btCountdownStart.Location = new System.Drawing.Point(144, 4);
            this.btCountdownStart.Name = "btCountdownStart";
            this.btCountdownStart.Size = new System.Drawing.Size(102, 23);
            this.btCountdownStart.TabIndex = 0;
            this.btCountdownStart.Text = "Start countdown";
            this.btCountdownStart.UseVisualStyleBackColor = true;
            this.btCountdownStart.Click += new System.EventHandler(this.btCountdownStart_Click);
            // 
            // tbCountdownTime
            // 
            this.tbCountdownTime.Location = new System.Drawing.Point(6, 6);
            this.tbCountdownTime.Name = "tbCountdownTime";
            this.tbCountdownTime.Size = new System.Drawing.Size(100, 20);
            this.tbCountdownTime.TabIndex = 1;
            this.tbCountdownTime.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "[ms]";
            // 
            // lblCountdownInfo
            // 
            this.lblCountdownInfo.AutoSize = true;
            this.lblCountdownInfo.Location = new System.Drawing.Point(141, 48);
            this.lblCountdownInfo.Name = "lblCountdownInfo";
            this.lblCountdownInfo.Size = new System.Drawing.Size(117, 13);
            this.lblCountdownInfo.TabIndex = 3;
            this.lblCountdownInfo.Text = "Countdown not started.";
            // 
            // loggingBox1
            // 
            this.loggingBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.loggingBox1.AutoSelectLastEntry = true;
            this.loggingBox1.BackColor = System.Drawing.SystemColors.Control;
            this.loggingBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loggingBox1.ClearButton = true;
            this.loggingBox1.CopyButton = true;
            this.loggingBox1.Location = new System.Drawing.Point(6, 6);
            this.loggingBox1.MaxEntryCount = 500;
            this.loggingBox1.Name = "loggingBox1";
            this.loggingBox1.SaveButtonVisible = true;
            this.loggingBox1.Size = new System.Drawing.Size(446, 260);
            this.loggingBox1.TabIndex = 0;
            this.loggingBox1.Title = "Logbook";
            // 
            // serialPortControl1
            // 
            this.serialPortControl1.Location = new System.Drawing.Point(9, 92);
            this.serialPortControl1.Name = "serialPortControl1";
            this.serialPortControl1.Size = new System.Drawing.Size(223, 55);
            this.serialPortControl1.TabIndex = 3;
            this.serialPortControl1.Load += new System.EventHandler(this.serialPortControl1_Load);
            this.serialPortControl1.OnByteReceived += new de.christianleberfinger.dotnet.pocketknife.IO.SerialPort.ByteReceivedHandler(this.serialPortControl1_OnByteReceived);
            // 
            // serialPort1
            // 
            this.serialPort1.OnByteReceived += new de.christianleberfinger.dotnet.pocketknife.IO.SerialPort.ByteReceivedHandler(this.serialPort1_OnByteReceived_1);
            this.serialPort1.OnConnectionStateChange += new de.christianleberfinger.dotnet.pocketknife.IO.SerialPort.ConnectionStateChangedHandler(this.serialPort1_OnConnectionStateChange);
            // 
            // btCountdownCancel
            // 
            this.btCountdownCancel.Location = new System.Drawing.Point(252, 4);
            this.btCountdownCancel.Name = "btCountdownCancel";
            this.btCountdownCancel.Size = new System.Drawing.Size(75, 23);
            this.btCountdownCancel.TabIndex = 4;
            this.btCountdownCancel.Text = "Cancel";
            this.btCountdownCancel.UseVisualStyleBackColor = true;
            this.btCountdownCancel.Click += new System.EventHandler(this.btCountdownCancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 321);
            this.Controls.Add(this.tabControlMain);
            this.Name = "Form1";
            this.Text = "pocketknife test application";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageLogging.ResumeLayout(false);
            this.tabPageSerialPort.ResumeLayout(false);
            this.tabPageSerialPort.PerformLayout();
            this.tabPageConfiguration.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBitIndex)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private de.christianleberfinger.dotnet.pocketknife.controls.LogBox loggingBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private de.christianleberfinger.dotnet.pocketknife.IO.SerialPort serialPort1;
        private de.christianleberfinger.dotnet.pocketknife.controls.SerialPortControl serialPortControl1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageLogging;
        private System.Windows.Forms.TabPage tabPageSerialPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageConfiguration;
        private System.Windows.Forms.PropertyGrid propertyGridConfig;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCalcOne;
        private System.Windows.Forms.Button btCalcThree;
        private System.Windows.Forms.Button btCalcTwo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbBitResult;
        private System.Windows.Forms.TextBox tbBitValue;
        private System.Windows.Forms.Button btMask;
        private System.Windows.Forms.NumericUpDown tbBitIndex;
        private System.Windows.Forms.TextBox tbBitMaskComplete;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbCountdownTime;
        private System.Windows.Forms.Button btCountdownStart;
        private System.Windows.Forms.Label lblCountdownInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCountdownCancel;
    }
}

