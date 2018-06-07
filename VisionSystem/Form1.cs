using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace VisionSystem
{
    public partial class Form1 : Form
    {
        VideoCapture cameraCapture;
        Image<Bgr, Byte> currentFrame;
        Image<Hsv, Byte> hsv = null;
        Image<Ycc, Byte> ycc = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraCapture == null)
            {
                cameraCapture = new VideoCapture(0);
                cameraCapture.QueryFrame();
                
                Application.Idle += Capture_ImageGrabbed;
            }
            //cameraCapture.ImageGrabbed += Capture_ImageGrabbed;
            //cameraCapture.Start();
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                currentFrame = cameraCapture.QueryFrame().ToImage<Bgr, Byte>();
                ycc = currentFrame.Convert<Ycc, Byte>();
                
                pictureBox1.Image = currentFrame.Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
                pictureBox2.Image = FindColor(ycc).Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
                textBox1.Text = currentFrame.Bitmap.Size.Width.ToString();
            }
            catch (Exception)
            {

            }
        }

        public Image<Gray, Byte> FindColor(Image<Ycc, Byte> yccimage)
        {
            Image<Ycc, Byte> ret = yccimage;
            var image = yccimage.InRange(new Ycc(30, 135, 85), new Ycc(235, 240, 240));
            var mat = yccimage.Mat;
            mat.SetTo(new MCvScalar(0), image);
            Image<Gray,Byte> gray = mat.ToImage<Gray, Byte>();
            gray = gray.ThresholdBinary(new Gray(50), new Gray(255)).Not();
            gray = gray.Dilate(6);

            mat.CopyTo(ret);
            return gray;
        }
        

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraCapture != null)
            {
                cameraCapture = null;
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
           
                Rectangle ee = new Rectangle(1, 1, Convert.ToInt32(pictureBox2.Width / 1.5), Convert.ToInt32(pictureBox2.Height / 1.5));
                using (Pen pen = new Pen(Color.Red, 1))
                {
                    e.Graphics.DrawRectangle(pen, ee);
                }
        }
    }
}
