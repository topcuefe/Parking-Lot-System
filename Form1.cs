
using System.Configuration;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Parking Lot GUI
namespace OtoparkArayuz
{

    public partial class Form1 : Form
    {

        // this SerialPort come from System.IO.Ports Library 
        // I used this because our project working with Arduino and sensors
        private SerialPort serialPort;


        int second, minute, hour;
        int second2, minute2, hour2;
        int second3, minute3, hour3;

        public Form1()
        {
            InitializeComponent();

            // I needed this part because time variables should not be assigned 
            timer1.Enabled = false;
            timer1.Tick += timer1_Tick;
            timer2.Enabled = false;
            timer2.Tick += timer2_Tick;
            timer3.Enabled = false;
            timer3.Tick += timer3_Tick;


            InitializeSerialPort();
        }

        private void InitializeSerialPort()
        {

            // In this line, I entered which port the Arduio is connected to 
            // and also which baudrate it is at to read the data.
            serialPort = new SerialPort("COM4", 9600);
            serialPort.NewLine = "\n";
            serialPort.DataReceived += SerialPort_DataReceived;

            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seri port açýlamadý: " + ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                //In this line, I assigned all the data in the line to the dataLine variable
                string dataLine = serialPort.ReadLine();


                dataLine = dataLine.Trim();

                // if we take true_1 or false_1 this effects parking lot 3 
                // if we take true_2 or false_2 this effects parking lot 5
                this.BeginInvoke(new Action(() =>
                {
                    if (dataLine == "false_1")
                    {
                        panel15.BackColor = Color.FromArgb(47, 47, 47);
                        label3.ForeColor = Color.FromArgb(0, 200, 83);
                        timer1.Enabled = false;
                    }
                    else if (dataLine == "true_1")
                    {
                        timer1.Enabled = true;
                        panel15.BackColor = Color.Red;
                        label3.ForeColor = Color.White;
                    }
                    else if (dataLine == "false_2")
                    {
                        panel13.BackColor = Color.FromArgb(47, 47, 47);
                        label5.ForeColor = Color.FromArgb(0, 200, 83);
                        timer2.Enabled = false;
                    }
                    else if (dataLine == "true_2")
                    {
                        timer2.Enabled = true;
                        timer3.Enabled = true;
                        panel27.BackColor = Color.Red;
                        label9.ForeColor = Color.White;
                        panel13.BackColor = Color.Red;
                        label5.ForeColor = Color.White;
                    }
                }));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data read error  " + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second++;

            if (second >= 60)
            {
                second = 0;
                minute++;

                if (minute >= 60)
                {
                    minute = 0;
                    hour++;
                }
            }
            label13.Text = string.Format("{0:00}:{1:00}:{2:00}", hour.ToString(), minute.ToString(), second.ToString());

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            second2++;

            if (second2 >= 60)
            {
                second2 = 0;
                minute2++;

                if (minute2 >= 60)
                {
                    minute2 = 0;
                    hour2++;
                }
            }
            label11.Text = string.Format("{0:00}:{1:00}:{2:00}", hour2.ToString(), minute2.ToString(), second2.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {

            second = minute = hour = 0;
            label13.Text = string.Format("{0:00}:{1:00}:{2:00}", hour.ToString(), minute.ToString(), second.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            second2 = minute2 = hour2 = 0;
            label11.Text = string.Format("{0:00}:{1:00}:{2:00}", hour2.ToString(), minute2.ToString(), second2.ToString());
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            second3++;

            if (second3 >= 60)
            {
                second3 = 0;
                minute3++;

                if (minute3 >= 60)
                {
                    minute3 = 0;
                    hour3++;
                }
            }
            label29.Text = string.Format("{0:00}:{1:00}:{2:00}", hour3.ToString(), minute3.ToString(), second3.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            second3 = minute3 = hour3 = 0;
            label29.Text = string.Format("{0:00}:{1:00}:{2:00}", hour3.ToString(), minute3.ToString(), second3.ToString());
        }
    }
}
