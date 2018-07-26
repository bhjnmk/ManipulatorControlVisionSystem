using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using Emgu.CV;
using Emgu.CV.Structure;
using AForge.Video;
using VisionSystem.RobotControl;

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
        string camera = "embeded";
        #endregion

        #region RobotVariables 
        string typeOfMove = "J01";
        SerialPort serialPort = new SerialPort();
        #endregion

        #region Classifiers
        CascadeClassifier fistClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/fist.xml");
        CascadeClassifier palmClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/palm.xml");
        //CascadeClassifier rockClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/rock.xml");

        Rectangle[] rectanglesFist;
        Rectangle[] rectanglesPalm;
        Rectangle[] rectanglesRock;
        #endregion


        #region Contructor
        public AppWin()
        {
            InitializeComponent();
            Size = new Size(1080, 720);
            StartPosition = FormStartPosition.CenterScreen;
            picBoxCameraView.SizeMode = PictureBoxSizeMode.StretchImage;

            foreach (String port in SerialPort.GetPortNames())
            {
                comSelect.Items.Add(port);
            }
        }
        #endregion

        #region WindowSet
        private void picBoxCameraView_Paint(object sender, PaintEventArgs e)
        {
            int box_width = Convert.ToInt32(picBoxCameraView.Width);
            int box_height = Convert.ToInt32(picBoxCameraView.Height);
            int rectangle_width = Convert.ToInt32(picBoxCameraView.Width / 3.5);
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

        #region Robot
        private void connect_btn_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.PortName = comSelect.Text;
                serialPort.BaudRate = Convert.ToInt32(baudSelect.Text);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), paritySelect.Text);
                serialPort.DataBits = Convert.ToInt32(dataSelect.Text);
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopSelect.Text);

                StartCommunication.StartComunicationWithRobot(serialPort);
            }
            catch (Exception)
            {

                MessageBox.Show("Wybierz wartości dla wszystkich elementów");
            }
        }
        #endregion

        #region WindowEvents
        private void AppWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;SRVOFF" + "\r\n");
                serialPort.Close();
            }

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

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                int sizeOfPicBox = Convert.ToInt32(picBoxCameraView.Height);

                currentFrame = cameraCapture.QueryFrame().ToImage<Bgr, Byte>();
                yccFrame = currentFrame.Convert<Ycc, Byte>();
                Image<Gray, Byte> grayFrame = yccFrame.Convert<Gray, Byte>();

                rectanglesFist = fistClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);
                rectanglesPalm = palmClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);
                //rectanglesRock = rockClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);

                foreach (var rectangle in rectanglesFist)
                {
                    yccFrame.Draw(rectangle, new Ycc(122, 222, 12));
                    richTextBox1.Text = String.Empty + "run";
                    //richTextBox2.Text = "X = " + rectangle.X + "Y = " + rectangle.Y;
                    MovingRobotXYZ.MoveRobotXYZ(rectangle, 200, richTextBox2, serialPort);
                }

                foreach (var rectangle2 in rectanglesPalm)
                {
                    yccFrame.Draw(rectangle2, new Ycc(1, 2, 255));
                }

                Bitmap bitmap = yccFrame.ToBitmap();
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                picBoxCameraView.Image = bitmap;
            }
            catch (Exception)
            { 
            }
        }

        private void Stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            picBoxCameraView.Image = bmp;
        }
        #endregion

        private void servoOnBtn_Click(object sender, EventArgs e)
        {
            StartStopServo.ServoOn(serialPort);
        }

        private void servoOffBtn_Click(object sender, EventArgs e)
        {
            StartStopServo.ServoOff(serialPort);
        }

        private void jointMoveBtn_Click(object sender, EventArgs e)
        {
            typeOfMove = "J00";
            jointMoveBtn.Enabled = false;
            xyzMoveBtn.Enabled = true;
        }

        private void xyzMoveBtn_Click(object sender, EventArgs e)
        {
            typeOfMove = "J01";
            jointMoveBtn.Enabled = true;
            xyzMoveBtn.Enabled = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SpeedControl.SpeedChange(trackBar1.Value,serialPort);
            
        }

        
    }
}
