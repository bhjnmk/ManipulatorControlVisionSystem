using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using AForge.Video;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;

namespace VisionSystem
{
    public partial class Form1 : Form
    {
        VideoCapture cameraCapture;
        Image<Bgr, Byte> currentFrame;
        Image<Ycc, Byte> ycc = null;
        Image<Ycc, Byte> ycc2 = null;
        Image<Ycc, Byte> findColor = null;
        Image<Gray, Byte> binary = null;
        Image<Gray, Byte> blurBinary = null;
        Image<Gray, byte> output;
        int largest_contour_index = 0;
        double largest_area = 0;

        MJPEGStream stream;
        string camera = "";
        int xLinear = 1;
        int yLinear = 1;
        int number = 3;
        string stop = "stop";
        string run = "run";
        private PaintEventArgs ee;



        static bool _continue;
        static SerialPort _serialPort;



        public Form1()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            
            if (camera == "embeded")
            {
                if (cameraCapture == null)
                {
                    cameraCapture = new VideoCapture(0);
                    cameraCapture.QueryFrame();
                    Application.Idle += Capture_ImageGrabbed;
                }
            }
            else if (camera == "ip")
            {

                //stream = new MJPEGStream("http://192.168.0.80:4747/video");
                //stream.NewFrame += stream_NewFrame;
                //stream.Start();

            }
        }

        private void stream_NewFrame(object sender, NewFrameEventArgs eventArgs, PaintEventArgs ee)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
        }

      

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                currentFrame = cameraCapture.QueryFrame().ToImage<Bgr, Byte>().Resize(0.9, Emgu.CV.CvEnum.Inter.Cubic);
                ycc = currentFrame.Convert<Ycc, Byte>();
                // findColor = FindColor(ycc);
                //binary = ChangeToBinary(findColor);
                //blurBinary = binary.SmoothMedian(5);
                //output = new Image<Gray, byte>(blurBinary.Width, blurBinary.Height, new Gray(0));
                pictureBox1.Image = ycc.ToBitmap();
                Image<Gray, Byte> gray = ycc.Convert<Gray, Byte>(); //Convert it to Grayscale
                pictureBox2.Image = gray.ToBitmap();
                CascadeClassifier fist = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/fist.xml");
                CascadeClassifier palm = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/palm.xml");

                 CascadeClassifier rock = new CascadeClassifier("C:/Users/Zajkos/source/VisionSystem/VisionSystem/classifier/gesture-rockNEW.xml");
                //CascadeClassifier okey = new CascadeClassifier("C:/OpenCV/opencv/build/x64/vc14/bin/test gesture/elephant_classifier.xml");

                //CascadeClassifier okey = new CascadeClassifier(" C:/Users/Zajkos/Desktop/gest rock/gesture-okNEW.xml.xml");

                // CascadeClassifier victoria = new CascadeClassifier("C:/Users/Zajkos/source/VisionSystem/VisionSystem/classifier/gesture-victoria.xml");

                Rectangle[] rectangles;
                Rectangle[] rectangles2;
                Rectangle[] rectangles3;
                Rectangle[] rectangles4;
                Rectangle[] rectangles5;

                // detect eyes.
                rectangles = fist.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
                rectangles2 = palm.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
               rectangles3 = rock.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
                //rectangles4 = okey.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
                //rectangles5 = victoria.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);


                foreach (var rectangle in rectangles)
                    {
                    ycc.Draw(rectangle, new Ycc(122,222,12));
                    richTextBox1.Text = String.Empty + "run";
                    //richTextBox1.Text = richTextBox1.Text + xLinear.ToString();
                    

                }
                foreach (var rectangle2 in rectangles2)
                {
                    //if (rectangle2.Height >= 80)
                    //{
                        ycc.Draw(rectangle2, new Ycc(1, 2, 255));
                       
                        richTextBox1.Text = String.Empty + "stop";
                    //richTextBox2.Text = String.Empty + rectangle2;

                    if (rectangle2.X >0 & rectangle2.X < 75 & rectangle2.Y >150 & rectangle2.Y <225)
                    {
                        richTextBox2.Text = String.Empty + "X minus";
                        
                    } else if (rectangle2.X > 425 & rectangle2.X < 500 & rectangle2.Y > 150 & rectangle2.Y < 225)
                    {
                        richTextBox2.Text = String.Empty + "X plus";

                    }
                    else if (rectangle2.X > 225 & rectangle2.X < 300 & rectangle2.Y > 300 & rectangle2.Y < 375)
                    {
                        richTextBox2.Text = String.Empty + "y minus";

                    }
                    else if (rectangle2.X > 225 & rectangle2.X < 300 & rectangle2.Y > 0 & rectangle2.Y < 75)
                    {
                        richTextBox2.Text = String.Empty + "y plus";

                    }
                    else if (rectangle2.X > 425 & rectangle2.X < 500 & rectangle2.Y > 0 & rectangle2.Y < 75)
                    {
                        richTextBox2.Text = String.Empty + "z minus";

                    }
                    else if (rectangle2.X > 0 & rectangle2.X < 75 & rectangle2.Y > 300 & rectangle2.Y < 375)
                    {
                        richTextBox2.Text = String.Empty + "z plus";

                    }
                    else if (rectangle2.X > 225 & rectangle2.X < 300 & rectangle2.Y > 150 & rectangle2.Y < 225)
                    {
                        richTextBox2.Text = String.Empty + "menu";

                    }
               
                    richTextBox2.Text = richTextBox2.Text + "\r\n" +"x:" + rectangle2.X + "y:"+ rectangle2.Y ;

                    //}
                }

                //foreach (var rectangle3 in rectangles3)
                //{
                   
                //    if (rectangle3.Height >= 80)
                //    {
                //        ycc.Draw(rectangle3, new Ycc(185, 148, 185));
                //        richTextBox1.Text = String.Empty + "rock";
                //        richTextBox2.Text = richTextBox2.Text + rectangle3;
                //    }
                      
                //}
                

                //foreach (var rectangle4 in rectangles4)
                //    {
                //    if (rectangle4.Height >= 100)
                //    {
                //        ycc.Draw(rectangle4, new Ycc(18, 12, 15));
                //        richTextBox1.Text = String.Empty + "ok";
                //               //richTextBox2.Text = richTextBox2.Text + rectangle4;
                //    }
                //}
                //foreach (var rectangle5 in rectangles5)
                //{
                //    // draw detected locations.

                //    ycc.Draw(rectangle5, new Ycc(138, 210, 138));
                //    richTextBox1.Text = String.Empty + "victoria";


                //}

                pictureBox3.Image = ycc.ToBitmap();
               
                
            }
            catch (Exception)
            {

            }
        }

       

        public Image<Ycc, Byte> FindColor(Image<Ycc, Byte> yccimage)
        {
            Image<Ycc, Byte> ret = yccimage;
            var image = yccimage.InRange(new Ycc(30, 135, 85), new Ycc(235, 240, 240));
            var mat = yccimage.Mat;
            mat.SetTo(new MCvScalar(0), image);
            mat.CopyTo(ret);

            return ret;
        }

        public Image<Gray, Byte> ChangeToBinary(Image<Ycc, Byte> fcimage)
        {
            var mat = fcimage.Mat;
            Image<Gray, Byte> gray = mat.ToImage<Gray, Byte>();
            gray = gray.ThresholdBinary(new Gray(50), new Gray(255)).Not();
            return gray;
        }
        
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraCapture != null)
            {
                cameraCapture = null;
            }
        }

       
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Rectangle yp = new Rectangle(225, 1, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));
            Rectangle ym = new Rectangle(225, 319, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));
            Rectangle xp = new Rectangle(1, 150, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));
            Rectangle xm = new Rectangle(460, 150, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));
            Rectangle zp = new Rectangle(460, 1, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));
            Rectangle zm = new Rectangle(1,319, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));

            Rectangle menu = new Rectangle(225, 150, Convert.ToInt32(pictureBox3.Width / 6), Convert.ToInt32(pictureBox3.Height / 6));
            using (Pen pen = new Pen(Color.Blue, 1))
            {
                e.Graphics.DrawRectangle(pen, yp);
                e.Graphics.DrawRectangle(pen, ym);
            }
            using (Pen pen = new Pen(Color.Red, 1))
            {
                e.Graphics.DrawRectangle(pen, xp);
                e.Graphics.DrawRectangle(pen, xm);
            }
            using (Pen pen = new Pen(Color.Green, 1))
            {
                e.Graphics.DrawRectangle(pen, zp);
                e.Graphics.DrawRectangle(pen, zm);
            }
            using (Pen pen = new Pen(Color.Orange, 1))
            {
                e.Graphics.DrawRectangle(pen, menu);
               
            }

        }
     

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopStream();
        }

        private void IPCamera_Click(object sender, EventArgs e)
        {
            camera = "ip";
        }

        private void EmbededCamera_Click(object sender, EventArgs e)
        {
            camera = "embeded";
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            stopStream();
        }

        private void stopStream()
        {
            if (cameraCapture != null)
            {
                cameraCapture = null;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //open tab rozpoznawanie
        }

        private void rozpoznawaniePictureBox_Click(object sender, EventArgs e)
        {
            //add event after click open rozpoznawanie tab
        }

        private void parametryPictureBox_Click(object sender, EventArgs e)
        {
            //add event after click open parametry tab
        }

        private void ustawieniaPictureBox_Click(object sender, EventArgs e)
        {
            //add event after click open ustawenia tab
        }

        private void pomocPictureBox_Click(object sender, EventArgs e)
        {
            //add event after click open pomoc tab
        }

        private void servoOnBtn_Click(object sender, EventArgs e)
        {
            //R3 send servo off comand
        }

        private void servoOffBtn_Click(object sender, EventArgs e)
        {
            //R3 send servo off comand
        }

        private void jointMoveBtn_Click(object sender, EventArgs e)
        {
            //R3 send joint move comand
        }

        private void xyzMoveBtn_Click(object sender, EventArgs e)
        {
            //R3 send xyz move comand
        }


        // OGARNAC KOMUNIKACJE SERIAL PORT
       /* public static void Main()
        {
            string name;
            string message;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            Thread readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = SetPortName(_serialPort.PortName);
            _serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);
            _serialPort.Parity = SetPortParity(_serialPort.Parity);
            _serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);
            _serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
            _serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
            _continue = true;
            readThread.Start();

            Console.Write("Name: ");
            name = Console.ReadLine();

            Console.WriteLine("Type QUIT to exit");

            while (_continue)
            {
                message = Console.ReadLine();

                if (stringComparer.Equals("quit", message))
                {
                    _continue = false;
                }
                else
                {
                    _serialPort.WriteLine(
                        String.Format("<{0}>: {1}", name, message));
                }
            }

            readThread.Join();
            _serialPort.Close();
        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }

        // Display Port values and prompt user to enter a port.
        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }
        // Display BaudRate values and prompt user to enter a value.
        public static int SetPortBaudRate(int defaultPortBaudRate)
        {
            string baudRate;

            Console.Write("Baud Rate(default:{0}): ", defaultPortBaudRate);
            baudRate = Console.ReadLine();

            if (baudRate == "")
            {
                baudRate = defaultPortBaudRate.ToString();
            }

            return int.Parse(baudRate);
        }

        // Display PortParity values and prompt user to enter a value.
        public static Parity SetPortParity(Parity defaultPortParity)
        {
            string parity;

            Console.WriteLine("Available Parity options:");
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Parity value (Default: {0}):", defaultPortParity.ToString(), true);
            parity = Console.ReadLine();

            if (parity == "")
            {
                parity = defaultPortParity.ToString();
            }

            return (Parity)Enum.Parse(typeof(Parity), parity, true);
        }
        // Display DataBits values and prompt user to enter a value.
        public static int SetPortDataBits(int defaultPortDataBits)
        {
            string dataBits;

            Console.Write("Enter DataBits value (Default: {0}): ", defaultPortDataBits);
            dataBits = Console.ReadLine();

            if (dataBits == "")
            {
                dataBits = defaultPortDataBits.ToString();
            }

            return int.Parse(dataBits.ToUpperInvariant());
        }

        // Display StopBits values and prompt user to enter a value.
        public static StopBits SetPortStopBits(StopBits defaultPortStopBits)
        {
            string stopBits;

            Console.WriteLine("Available StopBits options:");
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter StopBits value (None is not supported and \n" +
             "raises an ArgumentOutOfRangeException. \n (Default: {0}):", defaultPortStopBits.ToString());
            stopBits = Console.ReadLine();

            if (stopBits == "")
            {
                stopBits = defaultPortStopBits.ToString();
            }

            return (StopBits)Enum.Parse(typeof(StopBits), stopBits, true);
        }
        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            string handshake;

            Console.WriteLine("Available Handshake options:");
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            handshake = Console.ReadLine();

            if (handshake == "")
            {
                handshake = defaultPortHandshake.ToString();
            }

            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }*/
    }
}
