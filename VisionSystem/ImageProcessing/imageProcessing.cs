using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace VisionSystem.ImageProcessing
{
    class imageProcessing
    {
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
    }
}
