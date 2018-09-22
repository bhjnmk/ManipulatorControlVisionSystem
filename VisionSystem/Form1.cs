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
<<<<<<< HEAD
        string camera = "embeded";
=======
        string camera = "";
        int xLinear = 1;
        int yLinear = 1;
        int number = 3;
        string stop = "stop";
        string run = "run";
        //float scale = 0;
        int neighbour = 0;
>>>>>>> 44c4bc8fb8056b8c3f12b45838d513b1c7660019
        #endregion

        #region RobotVariables 
        string typeOfMove = "J01";
        SerialPort serialPort = new SerialPort();
        string[] splitPosition = new string[100];

        public delegate void ShowReceivedDataDelegate(string dataFromRobot);
        public ShowReceivedDataDelegate showDataDelegate;
        #endregion

<<<<<<< HEAD
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

=======
        #region Classifier
        // run 
        CascadeClassifier fistClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/fist.xml");
        // stop 
        CascadeClassifier palmClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.4.1.2976/etc/haarcascades/palm.xml");
        // increase speed
        CascadeClassifier testClassifier = new CascadeClassifier("C:/Users/Zajkos/source/VisionSystem/VisionSystem/classifier/rock-1h.xml");
        // decrease speed
        //CascadeClassifier okClassifier = new CascadeClassifier("C:/Users/Zajkos/source/VisionSystem/VisionSystem/classifier/gesture-rockNEW.xml");
        // xyz move typ

        // join move typ


        Rectangle[] rectanglesFist;
        Rectangle[] rectanglesPalm;
        Rectangle[] rectanglesTest;
        Rectangle[] rectanglesOk;
        #endregion



>>>>>>> 44c4bc8fb8056b8c3f12b45838d513b1c7660019
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
<<<<<<< HEAD
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
=======
                yccFrame = currentFrame.Convert<Ycc, Byte>();
                pictureBox1.Image = yccFrame.ToBitmap();
                Image<Gray, Byte> grayFrame = yccFrame.Convert<Gray, Byte>();
                pictureBox2.Image = grayFrame.ToBitmap();
                
                rectanglesFist = fistClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);
                rectanglesPalm = palmClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.2, minNeighbors: 12);
                rectanglesTest = testClassifier.DetectMultiScale(grayFrame, scaleFactor: 1.10, minNeighbors: neighbour, minSize :default(Size), maxSize : default(Size));
                
                foreach (var rectangle in rectanglesFist)
                {
                    yccFrame.Draw(rectangle, new Ycc(122,222,12));
                   
                   
                    string moveTo = "";

                    if (rectangle.X > 0 & rectangle.X < 75 & rectangle.Y > 150 & rectangle.Y < 225)
                    {
                        richTextBox2.Text = String.Empty + "X minus";
                        moveTo = "1;1;" + typeOfMove + "; 00;01;00;00";
                        richTextBox2.Text = moveTo;

                    }
                    else if (rectangle.X > 425 & rectangle.X < 500 & rectangle.Y > 150 & rectangle.Y < 225)
                    {
                        richTextBox2.Text = String.Empty + "X plus";
                        moveTo = "1;1;" + typeOfMove + ";00;01;00";
                        richTextBox2.Text = moveTo;

                    }
                    else if (rectangle.X > 225 & rectangle.X < 300 & rectangle.Y > 300 & rectangle.Y < 375)
                    {
                        richTextBox2.Text = String.Empty + "y minus";
                        moveTo = "1;1;" + typeOfMove + ";00;02;00;00";
                        richTextBox2.Text = moveTo;

                    }
                    else if (rectangle.X > 225 & rectangle.X < 300 & rectangle.Y > 0 & rectangle.Y < 75)
                    {
                        richTextBox2.Text = String.Empty + "y plus";
                        moveTo = "1;1;" + typeOfMove + ";00;02;00";
                        richTextBox2.Text = moveTo;

                    }
                    else if (rectangle.X > 425 & rectangle.X < 500 & rectangle.Y > 0 & rectangle.Y < 75)
                    {
                        richTextBox2.Text = String.Empty + "z minus";
                        moveTo = "1;1;" + typeOfMove + ";00;04;00;00";
                        richTextBox2.Text = moveTo;

                    }
                    else if (rectangle.X > 0 & rectangle.X < 75 & rectangle.Y > 300 & rectangle.Y < 375)
                    {
                        richTextBox2.Text = String.Empty + "z plus";
                        moveTo = "1;1;" + typeOfMove + ";00;04;00";
                        richTextBox2.Text = moveTo;

                    }
                    else if (rectangle.X > 225 & rectangle.X < 300 & rectangle.Y > 150 & rectangle.Y < 225)
                    {
                        richTextBox2.Text = String.Empty + "menu";
                        servoOnBtn_Click(sender, e);

                    }

                    richTextBox3.Text = "FIST : x:" + rectangle.X + ",y:" + rectangle.Y;
                }

                foreach (var rectangle2 in rectanglesPalm)
                {
                   
                    yccFrame.Draw(rectangle2, new Ycc(1, 2, 255));

                    string moveTo = "";

                    if (rectangle2.X > 0 & rectangle2.X < 75 & rectangle2.Y >150 & rectangle2.Y <225)
                    {

                    } else if (rectangle2.X > 425 & rectangle2.X < 500 & rectangle2.Y > 150 & rectangle2.Y < 225)
                    {

                    }
                    else if (rectangle2.X > 225 & rectangle2.X < 300 & rectangle2.Y > 300 & rectangle2.Y < 375)
                    {
                        
                    }
                    else if (rectangle2.X > 225 & rectangle2.X < 300 & rectangle2.Y > 0 & rectangle2.Y < 75)
                    {
                        
                    }
                    else if (rectangle2.X > 425 & rectangle2.X < 500 & rectangle2.Y > 0 & rectangle2.Y < 75)
                    {
                    }
                    else if (rectangle2.X > 0 & rectangle2.X < 75 & rectangle2.Y > 300 & rectangle2.Y < 375)
                    {
                        

                    }
                    else if (rectangle2.X > 225 & rectangle2.X < 300 & rectangle2.Y > 150 & rectangle2.Y < 225)
                    {
                        servoOffBtn_Click(sender, e);

                    }

                    richTextBox3.Text = "PALM : x:" + rectangle2.X + ",y:" + rectangle2.Y;
                    
                }

                
                foreach (var rectangle3 in rectanglesTest)
                {

                    yccFrame.Draw(rectangle3, new Ycc(44, 44, 44));
                    
                    string moveTo = "";

                    if (rectangle3.X > 0 & rectangle3.X < 75 & rectangle3.Y > 150 & rectangle3.Y < 225)
                    {

                    }
                    else if (rectangle3.X > 425 & rectangle3.X < 500 & rectangle3.Y > 150 & rectangle3.Y < 225)
                    {

                    }
                    else if (rectangle3.X > 225 & rectangle3.X < 300 & rectangle3.Y > 300 & rectangle3.Y < 375)
                    {//decrease speed
                        trackBar1.Value = trackBar1.Value - 1;

                    }
                    else if (rectangle3.X > 225 & rectangle3.X < 300 & rectangle3.Y > 0 & rectangle3.Y < 75)
                    {//increase speed
                        trackBar1.Value = trackBar1.Value + 1;
                    }
                    else if (rectangle3.X > 425 & rectangle3.X < 500 & rectangle3.Y > 0 & rectangle3.Y < 75)
                    {
                    }
                    else if (rectangle3.X > 0 & rectangle3.X < 75 & rectangle3.Y > 300 & rectangle3.Y < 375)
                    {
>>>>>>> 44c4bc8fb8056b8c3f12b45838d513b1c7660019

        private void Stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            picBoxCameraView.Image = bmp;
        }
        #endregion

<<<<<<< HEAD
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
=======
                    }
                    else if (rectangle3.X > 225 & rectangle3.X < 300 & rectangle3.Y > 150 & rectangle3.Y < 225)
                    {
                        //apply speed 
                        trackBar1_Scroll(sender, e);

                    }

                    richTextBox3.Text = "TEST : x:" + rectangle3.X + "y:" + rectangle3.Y;

                    //}
                }
                trackBar3_Scroll(sender, e);

                Bitmap bitmap = yccFrame.ToBitmap();
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                picBoxCameraView.Image = yccFrame.ToBitmap();
>>>>>>> 44c4bc8fb8056b8c3f12b45838d513b1c7660019

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

<<<<<<< HEAD
        #endregion

        #region JOINT Manual Control
        private void J1pBtn_Click(object sender, EventArgs e)
        {
            MovingRobotJoint.MoveRobotJoint(MovingRobotJoint.JointEnum.J1p, serialPort);
        }
=======
            string openComunication = "1;1;OPEN=melfa " + "\r\n";
            string servoOff = "1;1;SRVON" + "\r\n";
            richTextBox2.Text = openComunication + servoOff;
>>>>>>> 44c4bc8fb8056b8c3f12b45838d513b1c7660019

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


        private void glowne_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
       //     scale = 1 + (trackBar3.Value / 100);
       //     richTextBox2.Text = scale + "," + neighbour;
       }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            neighbour = trackBar3.Value;
            //string xx = neighbour + "";
            //richTextBox2.Text = xx;
        }
    }
}

