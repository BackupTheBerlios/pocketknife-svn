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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageLogging = new System.Windows.Forms.TabPage();
            this.tabPageSerialPort = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageConfiguration = new System.Windows.Forms.TabPage();
            this.propertyGridConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCalcOne = new System.Windows.Forms.Button();
            this.btCalcTwo = new System.Windows.Forms.Button();
            this.btCalcThree = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageLogging.SuspendLayout();
            this.tabPageSerialPort.SuspendLayout();
            this.tabPageConfiguration.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // serialPortControl1
            // 
            this.serialPortControl1.Location = new System.Drawing.Point(9, 92);
            this.serialPortControl1.Name = "serialPortControl1";
            this.serialPortControl1.Size = new System.Drawing.Size(223, 55);
            this.serialPortControl1.TabIndex = 3;
            this.serialPortControl1.Load += new System.EventHandler(this.serialPortControl1_Load);
            this.serialPortControl1.OnByteReceived += new de.christianleberfinger.dotnet.IO.SerialPort.ByteReceivedHandler(this.serialPortControl1_OnByteReceived);
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
            // serialPort1
            // 
            this.serialPort1.OnByteReceived += new de.christianleberfinger.dotnet.IO.SerialPort.ByteReceivedHandler(this.serialPort1_OnByteReceived_1);
            this.serialPort1.OnConnectionStateChange += new de.christianleberfinger.dotnet.IO.SerialPort.ConnectionStateChangedHandler(this.serialPort1_OnConnectionStateChange);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageLogging);
            this.tabControlMain.Controls.Add(this.tabPageSerialPort);
            this.tabControlMain.Controls.Add(this.tabPageConfiguration);
            this.tabControlMain.Controls.Add(this.tabPage1);
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
            this.tabPageSerialPort.Controls.Add(this.serialPortControl1);
            this.tabPageSerialPort.Controls.Add(this.button2);
            this.tabPageSerialPort.Controls.Add(this.button1);
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
            this.groupBox1.Size = new System.Drawing.Size(334, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculator";
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
            this.ResumeLayout(false);

        }

        #endregion

        private de.christianleberfinger.dotnet.controls.LogBox loggingBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private de.christianleberfinger.dotnet.IO.SerialPort serialPort1;
        private de.christianleberfinger.dotnet.controls.SerialPortControl serialPortControl1;
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
    }
}

