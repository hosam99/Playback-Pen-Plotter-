namespace Final_Project_Interface
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFileName = new System.Windows.Forms.Button();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxYCoordinate = new System.Windows.Forms.TextBox();
            this.textBoxXCoordinate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxStepperDelay = new System.Windows.Forms.TextBox();
            this.buttonSendStepperDelay = new System.Windows.Forms.Button();
            this.textBoxdelay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonPlayback = new System.Windows.Forms.Button();
            this.checkBoxRealTime = new System.Windows.Forms.CheckBox();
            this.buttonZero = new System.Windows.Forms.Button();
            this.UpArrow = new System.Windows.Forms.Label();
            this.DownArrow = new System.Windows.Forms.Label();
            this.RightArrow = new System.Windows.Forms.Label();
            this.LeftArrow = new System.Windows.Forms.Label();
            this.buttonZeroAxes = new System.Windows.Forms.Button();
            this.buttonMoveToZero = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(1396, 552);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(470, 38);
            this.txtFileName.TabIndex = 12;
            // 
            // btnFileName
            // 
            this.btnFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFileName.Location = new System.Drawing.Point(1101, 500);
            this.btnFileName.Name = "btnFileName";
            this.btnFileName.Size = new System.Drawing.Size(274, 90);
            this.btnFileName.TabIndex = 11;
            this.btnFileName.Text = "Select File Name";
            this.btnFileName.UseVisualStyleBackColor = true;
            this.btnFileName.Click += new System.EventHandler(this.btnFileName_Click);
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(1396, 502);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(189, 35);
            this.checkBoxSaveToFile.TabIndex = 10;
            this.checkBoxSaveToFile.Text = "Save to File";
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            this.checkBoxSaveToFile.CheckedChanged += new System.EventHandler(this.checkBoxSaveToFile_CheckedChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1600, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 31);
            this.label2.TabIndex = 34;
            this.label2.Text = "Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1454, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 31);
            this.label1.TabIndex = 33;
            this.label1.Text = "X";
            // 
            // textBoxYCoordinate
            // 
            this.textBoxYCoordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxYCoordinate.Location = new System.Drawing.Point(1557, 373);
            this.textBoxYCoordinate.Multiline = true;
            this.textBoxYCoordinate.Name = "textBoxYCoordinate";
            this.textBoxYCoordinate.Size = new System.Drawing.Size(112, 79);
            this.textBoxYCoordinate.TabIndex = 32;
            this.textBoxYCoordinate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxXCoordinate
            // 
            this.textBoxXCoordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXCoordinate.Location = new System.Drawing.Point(1411, 374);
            this.textBoxXCoordinate.Multiline = true;
            this.textBoxXCoordinate.Name = "textBoxXCoordinate";
            this.textBoxXCoordinate.Size = new System.Drawing.Size(112, 78);
            this.textBoxXCoordinate.TabIndex = 31;
            this.textBoxXCoordinate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1125, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(269, 106);
            this.button1.TabIndex = 30;
            this.button1.Text = "Jump To \r\nRelative Position [cm]\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxStepperDelay
            // 
            this.textBoxStepperDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStepperDelay.Location = new System.Drawing.Point(1521, 232);
            this.textBoxStepperDelay.Multiline = true;
            this.textBoxStepperDelay.Name = "textBoxStepperDelay";
            this.textBoxStepperDelay.Size = new System.Drawing.Size(258, 78);
            this.textBoxStepperDelay.TabIndex = 41;
            this.textBoxStepperDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxStepperDelay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxStepperDelay_KeyDown);
            this.textBoxStepperDelay.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxStepperDelay_KeyUp);
            // 
            // buttonSendStepperDelay
            // 
            this.buttonSendStepperDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSendStepperDelay.Location = new System.Drawing.Point(1156, 231);
            this.buttonSendStepperDelay.Name = "buttonSendStepperDelay";
            this.buttonSendStepperDelay.Size = new System.Drawing.Size(338, 79);
            this.buttonSendStepperDelay.TabIndex = 40;
            this.buttonSendStepperDelay.Text = "Set Max Stepper Speed [us]";
            this.buttonSendStepperDelay.UseVisualStyleBackColor = true;
            this.buttonSendStepperDelay.Click += new System.EventHandler(this.buttonSendStepperDelay_Click);
            // 
            // textBoxdelay
            // 
            this.textBoxdelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxdelay.Location = new System.Drawing.Point(1704, 374);
            this.textBoxdelay.Multiline = true;
            this.textBoxdelay.Name = "textBoxdelay";
            this.textBoxdelay.Size = new System.Drawing.Size(110, 78);
            this.textBoxdelay.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1698, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 31);
            this.label5.TabIndex = 44;
            this.label5.Text = "Rate [ms]";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Location = new System.Drawing.Point(36, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 1000);
            this.panel1.TabIndex = 50;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.MistyRose;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.Location = new System.Drawing.Point(782, 3);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(218, 53);
            this.buttonClear.TabIndex = 51;
            this.buttonClear.Text = "Clear Canvas";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonPlayback
            // 
            this.buttonPlayback.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonPlayback.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlayback.Location = new System.Drawing.Point(1450, 655);
            this.buttonPlayback.Name = "buttonPlayback";
            this.buttonPlayback.Size = new System.Drawing.Size(154, 53);
            this.buttonPlayback.TabIndex = 52;
            this.buttonPlayback.Text = "PlayBack";
            this.buttonPlayback.UseVisualStyleBackColor = false;
            this.buttonPlayback.Click += new System.EventHandler(this.buttonPlayback_Click);
            // 
            // checkBoxRealTime
            // 
            this.checkBoxRealTime.AutoSize = true;
            this.checkBoxRealTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRealTime.Location = new System.Drawing.Point(1512, 842);
            this.checkBoxRealTime.Name = "checkBoxRealTime";
            this.checkBoxRealTime.Size = new System.Drawing.Size(267, 35);
            this.checkBoxRealTime.TabIndex = 53;
            this.checkBoxRealTime.Text = "Real Time Tracing";
            this.checkBoxRealTime.UseVisualStyleBackColor = true;
            this.checkBoxRealTime.CheckedChanged += new System.EventHandler(this.checkBoxRealTime_CheckedChanged);
            // 
            // buttonZero
            // 
            this.buttonZero.BackColor = System.Drawing.Color.LightGreen;
            this.buttonZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZero.Location = new System.Drawing.Point(1434, 748);
            this.buttonZero.Name = "buttonZero";
            this.buttonZero.Size = new System.Drawing.Size(187, 53);
            this.buttonZero.TabIndex = 54;
            this.buttonZero.Text = "Extend Pen";
            this.buttonZero.UseVisualStyleBackColor = false;
            this.buttonZero.Click += new System.EventHandler(this.buttonZero_Click);
            // 
            // UpArrow
            // 
            this.UpArrow.AutoSize = true;
            this.UpArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpArrow.Location = new System.Drawing.Point(1219, 604);
            this.UpArrow.Name = "UpArrow";
            this.UpArrow.Size = new System.Drawing.Size(98, 108);
            this.UpArrow.TabIndex = 55;
            this.UpArrow.Text = "⇧";
            // 
            // DownArrow
            // 
            this.DownArrow.AutoSize = true;
            this.DownArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownArrow.Location = new System.Drawing.Point(1219, 736);
            this.DownArrow.Name = "DownArrow";
            this.DownArrow.Size = new System.Drawing.Size(98, 108);
            this.DownArrow.TabIndex = 56;
            this.DownArrow.Text = "⇩";
            // 
            // RightArrow
            // 
            this.RightArrow.AutoSize = true;
            this.RightArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightArrow.Location = new System.Drawing.Point(1281, 659);
            this.RightArrow.Name = "RightArrow";
            this.RightArrow.Size = new System.Drawing.Size(112, 108);
            this.RightArrow.TabIndex = 57;
            this.RightArrow.Text = "⇨";
            // 
            // LeftArrow
            // 
            this.LeftArrow.AutoSize = true;
            this.LeftArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftArrow.Location = new System.Drawing.Point(1143, 659);
            this.LeftArrow.Name = "LeftArrow";
            this.LeftArrow.Size = new System.Drawing.Size(112, 108);
            this.LeftArrow.TabIndex = 58;
            this.LeftArrow.Text = "⇦";
            // 
            // buttonZeroAxes
            // 
            this.buttonZeroAxes.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonZeroAxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZeroAxes.Location = new System.Drawing.Point(1663, 655);
            this.buttonZeroAxes.Name = "buttonZeroAxes";
            this.buttonZeroAxes.Size = new System.Drawing.Size(167, 53);
            this.buttonZeroAxes.TabIndex = 59;
            this.buttonZeroAxes.Text = "Zero Axes";
            this.buttonZeroAxes.UseVisualStyleBackColor = false;
            this.buttonZeroAxes.Click += new System.EventHandler(this.buttonZeroAxes_Click);
            // 
            // buttonMoveToZero
            // 
            this.buttonMoveToZero.BackColor = System.Drawing.Color.Thistle;
            this.buttonMoveToZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveToZero.Location = new System.Drawing.Point(1646, 748);
            this.buttonMoveToZero.Name = "buttonMoveToZero";
            this.buttonMoveToZero.Size = new System.Drawing.Size(201, 53);
            this.buttonMoveToZero.TabIndex = 60;
            this.buttonMoveToZero.Text = "Move to Zero";
            this.buttonMoveToZero.UseVisualStyleBackColor = false;
            this.buttonMoveToZero.Click += new System.EventHandler(this.buttonMoveToZero_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1883, 1049);
            this.Controls.Add(this.buttonMoveToZero);
            this.Controls.Add(this.buttonZeroAxes);
            this.Controls.Add(this.LeftArrow);
            this.Controls.Add(this.RightArrow);
            this.Controls.Add(this.DownArrow);
            this.Controls.Add(this.UpArrow);
            this.Controls.Add(this.buttonZero);
            this.Controls.Add(this.checkBoxRealTime);
            this.Controls.Add(this.buttonPlayback);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxdelay);
            this.Controls.Add(this.textBoxStepperDelay);
            this.Controls.Add(this.buttonSendStepperDelay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxYCoordinate);
            this.Controls.Add(this.textBoxXCoordinate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnFileName);
            this.Controls.Add(this.checkBoxSaveToFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFileName;
        private System.Windows.Forms.CheckBox checkBoxSaveToFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxYCoordinate;
        private System.Windows.Forms.TextBox textBoxXCoordinate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxStepperDelay;
        private System.Windows.Forms.Button buttonSendStepperDelay;
        private System.Windows.Forms.TextBox textBoxdelay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonPlayback;
        private System.Windows.Forms.CheckBox checkBoxRealTime;
        private System.Windows.Forms.Button buttonZero;
        private System.Windows.Forms.Label UpArrow;
        private System.Windows.Forms.Label DownArrow;
        private System.Windows.Forms.Label RightArrow;
        private System.Windows.Forms.Label LeftArrow;
        private System.Windows.Forms.Button buttonZeroAxes;
        private System.Windows.Forms.Button buttonMoveToZero;
    }
}

