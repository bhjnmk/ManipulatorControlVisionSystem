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
        Image<Hsv, Byte> hsvFrame = null;
        Image<Hsv, Byte> hsvFindColorFrame = null;
        Image<Gray, Byte> binaryFrame = null;

        //MJPEGStream stream;
        MJPEGStream stream;
        string camera = "embeded";
        #endregion

        #region RobotVariables 
        string typeOfMove = "J01";
        SerialPort serialPort = new SerialPort();
        string[] splitPosition = new string[100];

        public delegate void ShowReceivedDataDelegate(string dataFromRobot);
        public ShowReceivedDataDelegate showDataDelegate;
        #endregion

        #region Classifiers
        CascadeClassifier fistClassifier = new CascadeClassifier("C:/Users/Magda/Documents/Haar-features/cascade_fist.xml");
        CascadeClassifier palmClassifier = new CascadeClassifier("C:/Users/Magda/Documents/Haar-features/cascade_palm.xml");
        CascadeClassifier tombClassifier = new CascadeClassifier("C:/Users/Magda/Documents/Haar-features/cascade_tomb.xml");
        CascadeClassifier twoFingersClassifier = new CascadeClassifier("C:/Users/Magda/Documents/Haar-features/cascade_2fingers.xml");
        CascadeClassifier fingerClassifier = new CascadeClassifier("C:/Users/Magda/Documents/Haar-features/cascade_finger.xml");
        CascadeClassifier victoryClassifier = new CascadeClassifier("C:/Users/Magda/Documents/Haar-features/cascade_victory.xml");

        Rectangle[] rectanglesFist;
        Rectangle[] rectanglesPalm;
        Rectangle[] rectanglesTomb;
        Rectangle[] rectanglesTwoFingers;
        Rectangle[] rectanglesFinger;
        Rectangle[] rectanglesVictory;
        #endregion

        #region Contructor
        public AppWin()
        {
            InitializeComponent();
        }
        #endregion

        #region WindowSet
        private void AppWin_Load(object sender, EventArgs e)
        {
            Size = new Size(1080, 720);
            StartPosition = FormStartPosition.CenterScreen;
            picBoxCameraView.SizeMode = PictureBoxSizeMode.StretchImage;

            foreach (String port in SerialPort.GetPortNames())
            {
                comSelect.Items.Add(port);
            }

            showDataDelegate = new ShowReceivedDataDelegate(ShowReceivedDataMethod);
        }

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

        #region Connection with robot
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

                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                diconnect_btn.Visible = true;
                disconnect_rtxt.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się nawiązać połączenia. Sprawdź ustawienia połączenia.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.ToString(), "Error info", MessageBoxButtons.OK);
            }
        }

        public void ShowReceivedDataMethod(String dataFromRobot)
        {
            richTextBoxDataFromRobot.AppendText(dataFromRobot);
            string[] split = dataFromRobot.Split(new char[] { ';' });

            if (split[0] == "QoKX")
            {
                Xtxt.Text = split[1] + " mm";
                Ytxt.Text = split[3] + " mm";
                Ztxt.Text = split[5] + " mm";
            }
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sPort = (SerialPort)sender;
            string receivedData = sPort.ReadExisting();

            richTextBoxDataFromRobot.Invoke(showDataDelegate, new Object[] { receivedData });
        }

        private void receivedDataTimer_Tick(object sender, EventArgs e)
        {
            if (typeOfMove == "J01")
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Write("1;1;PPOSF" + "\r\n");
                }

                else
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Write("1;1;JPOSF" + "\r\n");
                    }
                }
            }
        }

        private void diconnect_btn_Click(object sender, EventArgs e)
        {
            CloseConnection();
            diconnect_btn.Visible = false;
            disconnect_rtxt.Visible = false;
        }
        #endregion

        #region WindowEvents
        private void AppWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            stopStream();
        }
        #endregion

        #region Methods
        public void CloseConnection()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;SRVOFF" + "\r\n");
                serialPort.Close();

            }
            MessageBox.Show("Zakończono połączenie z robotem", "Connection End", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Camera Set
        private void cameraIPBtn_Click(object sender, EventArgs e)
        {
            camera = "ip";
        }

        private void cameraEmbededBtn_Click(object sender, EventArgs e)
        {
            camera = "embeded";
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
        #endregion

        #region Gesture Recognition
        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                int sizeOfPicBox = Convert.ToInt32(picBoxCameraView.Height);

                currentFrame = cameraCapture.QueryFrame().ToImage<Bgr, Byte>();
                hsvFrame = currentFrame.Convert<Hsv, Byte>();
                hsvFindColorFrame = ImageProcessing.ImageProcessing.FindColor(hsvFrame);
                binaryFrame = ImageProcessing.ImageProcessing.ConvertImageToBinary(hsvFindColorFrame);

                rectanglesFist = fistClassifier.DetectMultiScale(binaryFrame, scaleFactor: 1.1, minNeighbors: 12);
                rectanglesPalm = palmClassifier.DetectMultiScale(binaryFrame, scaleFactor: 1.1, minNeighbors: 12);
                rectanglesTomb = tombClassifier.DetectMultiScale(binaryFrame, scaleFactor: 1.1, minNeighbors: 12);
                rectanglesTwoFingers = twoFingersClassifier.DetectMultiScale(binaryFrame, scaleFactor: 1.1, minNeighbors: 12);
                rectanglesFinger = fingerClassifier.DetectMultiScale(binaryFrame, scaleFactor: 1.1, minNeighbors: 12);
                rectanglesTwoFingers = victoryClassifier.DetectMultiScale(binaryFrame, scaleFactor: 1.1, minNeighbors: 12);

                foreach (var rectangle in rectanglesPalm)
                {
                    currentFrame.Draw(rectangle, new Bgr(50, 255, 50));
                    richTextBoxStatus.Text = string.Empty + "Run";
                    richTextBoxRecognitionData.Text = "x = " + rectangle.X + "y = " + rectangle.Y;
                    MovingRobotXYZ.MoveRobotXYZ(rectangle, richTextBoxRecognitionData, serialPort);
                }

                foreach (var rectangle in rectanglesFist)
                {
                    currentFrame.Draw(rectangle, new Bgr(255, 0, 0));
                    richTextBoxStatus.Text = string.Empty + "Stop";
                }

                foreach (var rectangle in rectanglesTomb)
                {
                    currentFrame.Draw(rectangle, new Bgr(128, 0, 128));
                    richTextBoxStatus.Text = string.Empty + "Chwytak ON";
                    SendingCommandsToRobot.SendCommand("HNDOFF1", serialPort);
                }

                foreach (var rectangle in rectanglesTwoFingers)
                {
                    currentFrame.Draw(rectangle, new Bgr(75, 0, 130));
                    richTextBoxStatus.Text = string.Empty + "Chwytak OFF";
                    SendingCommandsToRobot.SendCommand("HNDON1", serialPort);
                }

                foreach (var rectangle in rectanglesVictory)
                {
                    currentFrame.Draw(rectangle, new Bgr(0, 0, 128));
                    int actualSpeed = Convert.ToInt32(speedTxtBox.Text);
                    int newSpeed;
                    if (actualSpeed == 100)
                    {
                        newSpeed = 100;
                    }
                    else
                    {
                        newSpeed = actualSpeed + 10;
                    }

                    SpeedControl.SpeedChange(newSpeed, serialPort, speedTxtBox);
                }

                foreach (var rectangle in rectanglesFinger)
                {
                    currentFrame.Draw(rectangle, new Bgr(135, 200, 255));
                    int actualSpeed = Convert.ToInt32(speedTxtBox.Text);
                    int newSpeed;
                    if (actualSpeed == 0)
                    {
                        newSpeed = 0;
                    }
                    else
                    {
                        newSpeed = actualSpeed - 10;
                    }

                    SpeedControl.SpeedChange(newSpeed, serialPort, speedTxtBox);
                }

                Bitmap bitmap = currentFrame.ToBitmap();
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

        #region Robot Setting
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

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            TrackBar speedBar = (TrackBar)sender;
            if (speedBar.Name == "speedbarJoint")
            {
                speedJ_txt.Text = speedBar.Value.ToString() + " %";
            }
            else
            {
                speedXYX_txt.Text = speedBar.Value.ToString() + " %";
            }
            try
            {
                SpeedControl.SpeedChange(speedBar.Value, serialPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd!\n" + ex.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region XYZ Manual Control
        private void XplusBtn_Click(object sender, EventArgs e)
        {
            MovingRobotXYZ.MoveRobotXYZ(MovingRobotXYZ.XYZenum.Xp, serialPort);
        }

        private void XminusBtn_Click(object sender, EventArgs e)
        {
            MovingRobotXYZ.MoveRobotXYZ(MovingRobotXYZ.XYZenum.Xm, serialPort);
        }

        private void YplusBtn_Click(object sender, EventArgs e)
        {
            MovingRobotXYZ.MoveRobotXYZ(MovingRobotXYZ.XYZenum.Yp, serialPort);
        }

        private void YminusBtn_Click(object sender, EventArgs e)
        {
            MovingRobotXYZ.MoveRobotXYZ(MovingRobotXYZ.XYZenum.Ym, serialPort);
        }

        private void ZplusBtn_Click(object sender, EventArgs e)
        {
            MovingRobotXYZ.MoveRobotXYZ(MovingRobotXYZ.XYZenum.Zp, serialPort);
        }

        private void ZminusBtn_Click(object sender, EventArgs e)
        {
            MovingRobotXYZ.MoveRobotXYZ(MovingRobotXYZ.XYZenum.Zm, serialPort);
        }
        #endregion

        private void sendToRobotBtn_Click(object sender, EventArgs e)
        {
            SendingCommandsToRobot.SendCommand(richTextBoxCommandSend.Text, serialPort);
        }

        #region Chwytak

        private void chwytakOnBtn_Click(object sender, EventArgs e)
        {
            SendingCommandsToRobot.SendCommand("HNDOFF1", serialPort);
        }

        private void chwytakOffBtn_Click(object sender, EventArgs e)
        {
            SendingCommandsToRobot.SendCommand("HNDON1", serialPort);
        }

        #endregion

        #region JOINT Manual Control
        private void J1pBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J1p, serialPort);
        }

        private void J1mBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J1m, serialPort);
        }

        private void J2pBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J2p, serialPort);
        }

        private void J2mBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J2m, serialPort);
        }

        private void J3pBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J3p, serialPort);
        }

        private void J3mBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J3m, serialPort);
        }

        private void J5pBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J5p, serialPort);
        }


        private void J5mBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J5m, serialPort);
        }

        private void J6pBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J6p, serialPort);
        }

        private void J6mBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J6m, serialPort);
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;PPOSF" + "\r\n");
            }
        }
    }
}

