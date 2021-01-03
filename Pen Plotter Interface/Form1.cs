using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;


namespace Final_Project_Interface
{

    public partial class Form1 : Form
    {

        public struct dataStruct
        {
            public dataStruct(byte[] data)
            {
                x_upperDataByte = data[0];
                x_lowerDataByte = data[1];
                y_upperDataByte = data[2];
                y_lowerDataByte = data[3];
                escByte = data[4];


                if (escByte == 1) { x_lowerDataByte = 255; }
                else if (escByte == 2) { y_lowerDataByte = 255; }
                else if (escByte == 3) { y_lowerDataByte = x_lowerDataByte = 255; }
                else { }

                encoderCounts = (x_upperDataByte << 8) + x_lowerDataByte;
                stepCount = (y_upperDataByte << 8) + y_lowerDataByte;

            }
            public byte escByte { get; }
            public int encoderCounts { get; }
            public int stepCount { get; }
            public int x_upperDataByte { get; }
            public int x_lowerDataByte { get; }
            public int y_upperDataByte { get; }
            public int y_lowerDataByte { get; }

        }

        int px, py;
        double range;
        int[] data = new int[3];
        int[] prevdata = new int[3];
        byte[] dataPacket = new byte[5];
        bool ready = false, firsttime = true, realTime = false, moving = false, transmissionDone = false;

        Pen pen;
        Graphics g;
        StreamWriter outputFile;
        Stopwatch stopwatch1 = new Stopwatch();
        Stopwatch stopwatch2 = new Stopwatch();
        ConcurrentQueue<int[]> coordinates = new ConcurrentQueue<int[]>();
        ConcurrentQueue<int[]> tempCoordinates = new ConcurrentQueue<int[]>();


        public Form1()
        {
            range = 11.5;
            px = py = 0;
            InitializeComponent();
            g = panel1.CreateGraphics();
            pen = new Pen(Color.Black, 1.75f);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        /// <summary>
        /// Configure serial port upon form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //serialPort1.PortName = SerialPort.GetPortNames().ToArray().First().ToString();

            //while (!serialPort1.IsOpen)
            //{
            //    try
            //    {
            //        serialPort1.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}

        }

        /// <summary>
        /// Close serial port when closing the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }

        /// <summary>
        /// Sent new data when current data has been processed by MCU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            int counter = 0;

            if (coordinates.Count > 30) { transmitData(30, 0, 14, 0); }
            else if (coordinates.Count > 0) { transmitData(coordinates.Count, 1, 14, 0); }
            else{
                transmitData(0, 0, 5, 0);
                transmitData(0, 0, 13, 0);
                transmissionDone = true;
                while (tempCoordinates.TryDequeue(out data)) { coordinates.Enqueue(data); }
            }

            if (!transmissionDone)
            {
                while (counter < 30 && !coordinates.IsEmpty)
                {
                    coordinates.TryDequeue(out data);
                    tempCoordinates.Enqueue(data);
                    Console.WriteLine($"{data[0]}, {data[1]}, {data[2]}");
                    int x = (int)Math.Round((data[0] - prevdata[0]) * range * 4096 / panel1.Width / 3.9);
                    int y = (int)Math.Round((data[1] - prevdata[1]) * range * 4096 / panel1.Height / 3.9);
                    transmitData(x, y, 0, data[2]);
                    prevdata = data;
                    counter++;
                }
            }

        }

        private string getFilePath()
        {
            string filename = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }

            return filename;
        }

        /// <summary>
        /// Formats and writes the acquired data to the user selected file/ destination
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="az"></param>
        private void writeToFile(double ax, double ay, double x, double y, double az)
        {
            if (ready)
            {
                outputFile.WriteLine($"{ax}, {ay}, {x}, {y}, {az}");
            }
        }

        /// <summary>
        /// Either starts or stops acquiring data based on the status of the check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSaveToFile_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxSaveToFile.Checked)
            {
                if (txtFileName.Text != "")
                    outputFile = new StreamWriter(txtFileName.Text);
                else
                    outputFile = new StreamWriter(getFilePath());

                ready = true;
                outputFile.WriteLine($"X_10, Y_10, X_1, Y_1, Pen Status");

            }
            else
            {
                outputFile.Close();
                ready = false;
            }
        }

        /// <summary>
        /// Prompts the user to select a destination for saving the acquired data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileName_Click(object sender, EventArgs e)
        {
            txtFileName.Text = getFilePath();
        }

        /// <summary>
        /// Transmits some serial data packet to the MCU
        /// </summary>
        /// <param name="xdata"></param>
        /// <param name="ydata"></param>
        /// <param name="commandByte"></param>
        /// <param name="StatusByte"></param>
        private void transmitData(int xdata, int ydata, byte commandByte, int StatusByte)
        {

            byte data_x_upper = (byte)(Math.Abs(xdata) >> 8);
            byte data_y_upper = (byte)(Math.Abs(ydata) >> 8);
            byte data_x_lower = (byte)(Math.Abs(xdata));
            byte data_y_lower = (byte)(Math.Abs(ydata));
            byte[] data = { 255, 1, data_x_upper, data_x_lower, data_y_upper, data_y_lower, (byte)StatusByte };

            if (serialPort1.IsOpen)
            {
                if (commandByte == 0)
                {
                    if (xdata < 0 && ydata < 0) { data[1] = 4; }
                    else if (xdata < 0) { data[1] = 2; }
                    else if (ydata < 0) { data[1] = 3; }
                }
                else { data[1] = commandByte; }

                try
                {
                    serialPort1.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        /// <summary>
        /// Transmits some relative coordinates to the MCU in cm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            double x, y, L;
            x = Convert.ToDouble(textBoxXCoordinate.Text);
            y = Convert.ToDouble(textBoxYCoordinate.Text);

            transmitData((int)Math.Round(x * 4096 / 3.9), (int)Math.Round(y * 4096 / 3.9), 0,1);
            transmitData(0, 0, 0, 0);
            transmitData((int)Math.Round(x * -4096 / 3.9), (int)Math.Round(y * -4096 / 3.9), 0, 1);

        }

        /// <summary>
        /// Adjusts max stepper spped in terms of delay time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendStepperDelay_Click(object sender, EventArgs e)
        {
            transmitData(Convert.ToInt32(textBoxStepperDelay.Text), 0, 6,0);
        }

        /// <summary>
        /// Moves the two axes depending on the keyboard input (directional arrows)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxStepperDelay_KeyDown(object sender, KeyEventArgs e)
        {
            //if (firsttime) { firsttime = false; transmitData(0, 0, 12,0); }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    transmitData(0, 0, 10,0);
                    LeftArrow.ForeColor = Color.Blue;
                    break;
                case Keys.Up:
                    transmitData(0, 0, 7,0);
                    UpArrow.ForeColor = Color.Blue;
                    break;
                case Keys.Right:
                    transmitData(0, 0, 9,0);
                    RightArrow.ForeColor = Color.Blue;
                    break;
                case Keys.Down:
                    transmitData(0, 0, 8,0);
                    DownArrow.ForeColor = Color.Blue;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Stores x-y coordinates of mouse when first pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (firsttime) { px = py = 0; firsttime = false; }
            int[] p = new int[] { e.X, e.Y, 0 };

            if (realTime == false)
            {
                coordinates.Enqueue(p);
                writeToFile(p[0], p[1], p[0], p[1], p[2]);
            }
            else {
                int x = (int)Math.Round((e.X - px) * range * 4096 / panel1.Width / 3.9);
                int y = (int)Math.Round((e.Y - py) * range * 4096 / panel1.Height / 3.9);
                transmitData(x, y, 0, 0);
            }

            moving = true;
            px = e.X; py = e.Y;

        }

        /// <summary>
        /// Stores x-y coordinates of mouse at some interval as it is moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            stopwatch1.Start();
            stopwatch2.Start();

            if (moving && px != -1 && py != -1) {

                int[] p = new int[] { e.X, e.Y, 1 };
                
                g.DrawLine(pen, new Point(px, py), e.Location);

                if (stopwatch2.ElapsedMilliseconds >= 1)
                {
                    coordinates.Enqueue(p);
                    writeToFile(0,0, p[0], p[1], p[2]);
                    stopwatch2.Restart();
                }

                if (stopwatch1.ElapsedMilliseconds >= 10)
                {
                    if (realTime == false)
                    {
                        coordinates.Enqueue(p);
                        writeToFile(p[0], p[1],0,0, p[2]);
                    }
                    else {
                        int x = (int)Math.Round((e.X - px) * range * 4096 / panel1.Width / 3.9);
                        int y = (int)Math.Round((e.Y - py) * range * 4096 / panel1.Height / 3.9);
                        transmitData(x, y, 0, 1);
                    }
                    stopwatch1.Restart();
                }

                px = e.X; py = e.Y;
            }
        }

        /// <summary>
        /// Stores x-y coordinates of mouse when released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            int[] p = new int[] { e.X, e.Y, 1 };

            if (realTime == false)
            {
                coordinates.Enqueue(p);
                writeToFile(p[0], p[1], p[0], p[1], p[2]);
                //p[2] = 0;
                //coordinates.Enqueue(p);
                //writeToFile(p[0], p[1], p[2]);
            }
            else {
                int x = (int)Math.Round((e.X - px) * range * 4096 / panel1.Width / 3.9);
                int y = (int)Math.Round((e.Y - py) * range * 4096 / panel1.Height / 3.9);
                transmitData(x,y,0,1);
                transmitData(x, y, 0, 0);
            }

            moving = false;
            px = e.X; py = e.Y;

        }

        /// <summary>
        /// Initiates data transmission process for the accumulated coordinates of the drawn trajectory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayback_Click(object sender, EventArgs e)
        {
            int counter = 0,x,y;
            transmissionDone = false;
            //tempCoordinates = new ConcurrentQueue<int[]>();
            transmitData(35, 0, 14, 0);

            //if (firsttime)
            //{
            //coordinates.TryDequeue(out data);
            prevdata[0] = prevdata[1] = prevdata[2] = 0;
            //int x = (int)Math.Round((data[0] - prevdata[0]) * range * 4096 / panel1.Width / 3.9);
            //int y = (int)Math.Round((data[1] - prevdata[1]) * range * 4096 / panel1.Height / 3.9);
            //transmitData(x, y, 0, data[2]);
            //prevdata = data;
            //counter++; firsttime = false;

            //}

            while (counter < 35 && !coordinates.IsEmpty)
            {
                coordinates.TryDequeue(out data);
                tempCoordinates.Enqueue(data);
                x = (int)Math.Round((data[0] - prevdata[0]) * range * 4096 / panel1.Width / 3.9);
                y = (int)Math.Round((data[1] - prevdata[1]) * range * 4096 / panel1.Height / 3.9);
                transmitData(x, y, 0, data[2]);
                prevdata = data;
                counter++;
            }

        }

        /// <summary>
        /// Sends a command to MCU to zero axes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZero_Click(object sender, EventArgs e)
        {
            if (buttonZero.Text == "Extend Pen") { buttonZero.Text = "Retract Pen"; }
            else { buttonZero.Text = "Extend Pen"; }
            transmitData(0, 0, 5, 0);
        }

        private void buttonZeroAxes_Click(object sender, EventArgs e)
        {
            transmitData(0,0,12,0);
        }

        private void buttonMoveToZero_Click(object sender, EventArgs e)
        {
            transmitData(0,0,13,0);
        }
        private void checkBoxRealTime_CheckedChanged(object sender, EventArgs e)
        {
            clearPanel();
            if (checkBoxRealTime.Checked){ realTime = true; transmitData(0,0,15,0); }
            else { realTime = false; transmitData(0, 0, 16, 0); }
        }

        /// <summary>
        /// Clears Canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearPanel();
        }

        void clearPanel()
        {
            g.Clear(Color.White);
            coordinates = new ConcurrentQueue<int[]>();
        }

        private void textBoxStepperDelay_KeyUp(object sender, KeyEventArgs e)
        {
            transmitData(0, 0, 11,0);
            UpArrow.ForeColor = Color.Black;
            DownArrow.ForeColor = Color.Black;
            RightArrow.ForeColor = Color.Black;
            LeftArrow.ForeColor = Color.Black;
        }

    }
}
