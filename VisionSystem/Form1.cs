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
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using AForge.Video;

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
        int x = 1;
        int y = 1;
        int number = 3;
        string stop = "stop";
        string run = "run";
        int z = 0;
        int z2 = 0;
        int z3 = 0;
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

                stream = new MJPEGStream("http://192.168.0.80:4747/video");
                stream.NewFrame += stream_NewFrame;
                stream.Start();

            }
        }

        private void stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
        }

      

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                currentFrame = cameraCapture.QueryFrame().ToImage<Bgr, Byte>().Resize(0.4, Emgu.CV.CvEnum.Inter.Cubic);
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
                
                
                 CascadeClassifier okey = new CascadeClassifier("C:/Users/Zajkos/source/VisionSystem/VisionSystem/classifier/cascade-okey2.xml");

                Rectangle[] rectangles;
                Rectangle[] rectangles2;
                //Rectangle[] rectangles3;
                Rectangle[] rectangles4;

                // detect eyes.
                rectangles = fist.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
                rectangles2 = palm.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
                //rectangles3 = okey.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);
                rectangles4 = okey.DetectMultiScale(gray, scaleFactor: 1.2, minNeighbors: 12);


                foreach (var rectangle in rectangles)
                    {
                    // draw detected locations.
                    z = z + 1; //File.Create("C:/Emgu/test/my" + z + ".bmp");
                    ycc.Draw(rectangle, new Ycc(122,222,12));
                    richTextBox1.Text = String.Empty + "run";
                    // ycc.Save("test.jpg");
                    
                  // ycc.Save("C:/Emgu/test/my" + z + ".bmp"); 

    }
                foreach (var rectangle2 in rectangles2)
                {
                    // draw detected locations.

                    ycc.Draw(rectangle2, new Ycc(1, 2, 255));
                    richTextBox1.Text = String.Empty + "stop";

                    
                }
/*
                foreach (var rectangle3 in rectangles3)
                {
                    // draw detected locations.

                    ycc.Draw(rectangle3, new Ycc(185, 148, 185));
                    richTextBox1.Text = String.Empty + "okey";


                }*/
                foreach (var rectangle4 in rectangles4)
                {
                    // draw detected locations.

                    ycc.Draw(rectangle4, new Ycc(18, 12, 15));
                    richTextBox1.Text = String.Empty + "ok";


                }


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

        //public IOutputArray FindContours(Image<Gray,Byte> binaryImage)
        //{
        //    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        //    Mat hierarchy = new Mat();
        //    CvInvoke.FindContours(
        //        binaryImage,
        //        contours,
        //        hierarchy,
        //        RetrType.Ccomp,
        //        ChainApproxMethod.ChainApproxSimple
        //        );
        
        //    return contours;
        //}
        
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraCapture != null)
            {
                cameraCapture = null;
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
           
        //        Rectangle ee = new Rectangle(1, 1, Convert.ToInt32(pictureBox2.Width / 1.5), Convert.ToInt32(pictureBox2.Height / 1.5));
        //        using (Pen pen = new Pen(Color.Red, 1))
        //        {
        //            e.Graphics.DrawRectangle(pen, ee);
        //        }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            x = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            y = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            number = Convert.ToInt32(numericUpDown1.Value);
        }

        private void capture_btn_Click(object sender, EventArgs e)
        {
            if (camera == "embeded")
            {
                pictureBox5.Image = pictureBox1.Image;
                z = z + 1;
                ycc.Save("C:/Emgu/test/haar_classifier--okey" + z + ".jpg");

                if (cameraCapture == null)
                {
                    cameraCapture = new VideoCapture(0);
                    cameraCapture.QueryFrame();
                    Application.Idle += Capture_ImageGrabbed;
                }
            }
            else if (camera == "ip")
            {

                pictureBox5.Image = pictureBox1.Image;

            }
            //pictureBox3.Image = output.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
            //richTextBox2.Text = richTextBox2.Text + output.GetType();
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
           // stream.Stop();
            if (cameraCapture != null)
            {
                cameraCapture = null;
            }
        }
    }
}
