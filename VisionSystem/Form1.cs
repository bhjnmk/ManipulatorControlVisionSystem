using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using Emgu.CV;
using Emgu.CV.Structure;
using AForge.Video;

namespace VisionSystem
{
    public partial class AppWin : Form
    {
        #region CameraVariables
        VideoCapture cameraCapture;
        Image<Bgr, Byte> currentFrame;
        Image<Ycc, Byte> yccFrame = null;

        //MJPEGStream stream;
        MJPEGStream stream;
        string camera = "";
        int xLinear = 1;
        int yLinear = 1;
        int number = 3;
        string stop = "stop";
        string run = "run";
        #endregion

        #region Classifier
        CascadeClassifier fistClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/fist.xml");
        CascadeClassifier palmClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/palm.xml");
        CascadeClassifier rockClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/rock.xml");

        Rectangle[] rectanglesFist;
        Rectangle[] rectanglesPalm;
        Rectangle[] rectanglesRock;
        #endregion

        static bool _continue;
        static SerialPort _serialPort;

        #region Contructor
        public AppWin()
        {
            InitializeComponent();
            Size = new Size(1080, 720);
            StartPosition = FormStartPosition.CenterScreen;
            picBoxCameraView.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        #endregion

        #region WindowSet
        private void picBoxCameraView_Paint(object sender, PaintEventArgs e)
        {
            int box_width = Convert.ToInt32(picBoxCameraView.Width);
            int box_height = Convert.ToInt32(picBoxCameraView.Height);
            int rectangle_width = Convert.ToInt32(box_width / 3.5);
            int rectangle_height = Convert.ToInt32(box_height / 3.5);

            Rectangle yp = new Rectangle(box_width / 2 - rectangle_width / 2, 1, rectangle_width, rectangle_height);
            Rectangle ym = new Rectangle(box_width / 2 - rectangle_width / 2, box_height - rectangle_height - 1, rectangle_width, rectangle_height);
            Rectangle xm = new Rectangle(1, box_height / 2 - rectangle_height / 2, rectangle_width, rectangle_height);
            Rectangle xp = new Rectangle(box_width - rectangle_width - 1, box_height / 2 - rectangle_height / 2, rectangle_width, rectangle_height);
            Rectangle zp = new Rectangle(box_width - rectangle_width - 1, 1, rectangle_width, rectangle_height);
            Rectangle zm = new Rectangle(1, box_height - rectangle_height - 1, rectangle_width, rectangle_height);
            Rectangle menu = new Rectangle(box_width / 2 - rectangle_width / 2, box_height / 2 - rectangle_height / 2, rectangle_width, rectangle_height);

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
            using (Pen pen = new Pen(Color.Black, 1))
            {
                e.Graphics.DrawRectangle(pen, menu);

            }
        }
        #endregion

        #region WindowEvents
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
                stream = new MJPEGStream("http://192.168.0.80:4747/video");
                stream.NewFrame += Stream_NewFrame;
                stream.Start();
            }
        }

        private void Stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                currentFrame = cameraCapture.QueryFrame().ToImage<Bgr, Byte>();
                yccFrame = currentFrame.Convert<Ycc, Byte>();
                pictureBox1.Image = yccFrame.ToBitmap();
                Image<Gray, Byte> grayFrame = yccFrame.Convert<Gray, Byte>();
                pictureBox2.Image = grayFrame.ToBitmap();

                rectanglesFist = fistClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);
                rectanglesPalm = palmClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);
                rectanglesRock = rockClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);

                foreach (var rectangle in rectanglesFist)
                {
                    yccFrame.Draw(rectangle, new Ycc(122,222,12));
                    richTextBox1.Text = String.Empty + "run";
                    //richTextBox1.Text = richTextBox1.Text + xLinear.ToString();
                }

                foreach (var rectangle2 in rectanglesPalm)
                {
                   
                    yccFrame.Draw(rectangle2, new Ycc(1, 2, 255));
                       
                    richTextBox1.Text = String.Empty + "stop";
                    //richTextBox2.Text = String.Empty + rectangle2;

                    if (rectangle2.X > 0 & rectangle2.X < 75 & rectangle2.Y >150 & rectangle2.Y <225)
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
                Bitmap bitmap = yccFrame.ToBitmap();
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                picBoxCameraView.Image = bitmap;

            }
            catch (Exception)
            {

            }
        }
        #endregion

        public void whereIsHand()
        {


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
