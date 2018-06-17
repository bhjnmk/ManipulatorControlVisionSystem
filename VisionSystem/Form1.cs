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
                findColor = FindColor(ycc);
                binary = ChangeToBinary(findColor);
                blurBinary = binary.SmoothMedian(5);
                output = new Image<Gray, byte>(blurBinary.Width, blurBinary.Height, new Gray(0));

                pictureBox1.Image = blurBinary.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;

                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hierarchy = new Mat();
                CvInvoke.FindContours(
                    blurBinary,
                    contours,
                    hierarchy,
                    RetrType.External,
                    ChainApproxMethod.ChainApproxSimple
                    );

                    for (int i = 0; i < contours.Size; i++)
                    {
                        double a = CvInvoke.ContourArea(contours[i], false);
                    //if (a > largest_area)
                    //{
                    //    largest_area = a;
                    //    largest_contour_index = i;
                    //}
                    CvInvoke.DrawContours(output, contours, i, new MCvScalar(255, 0, 0), 2);
                }

                    //CvInvoke.DrawContours(output, contours, largest_contour_index, new MCvScalar(255, 0, 0), 2);

                    richTextBox1.Text = richTextBox1.Text + contours.Size + Environment.NewLine;
                //pictureBox1.Image = currentFrame.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
                //pictureBox2.Image = findColor.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
                //pictureBox3.Image = binary.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
                pictureBox2.Image = output.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;


                textBox1.Text = currentFrame.Bitmap.Size.Width.ToString();
                
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
                if (cameraCapture == null)
                {
                    cameraCapture = new VideoCapture(0);
                    cameraCapture.QueryFrame();
                    Application.Idle += Capture_ImageGrabbed;
                }
            }
            else if (camera == "ip")
            {

                pictureBox3.Image = pictureBox1.Image;

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
            stream.Stop();
            if (cameraCapture != null)
            {
                cameraCapture = null;
            }
        }
    }
}
