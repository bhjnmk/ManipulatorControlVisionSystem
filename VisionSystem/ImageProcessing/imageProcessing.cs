using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace VisionSystem.ImageProcessing
{
    public static class ImageProcessing
    {
        public static Image<Hsv, Byte> FindColor(Image<Hsv, Byte> hsvImage)
        {
            Image<Hsv, Byte> ret = hsvImage;
            //blue
            var image = hsvImage.InRange(new Hsv(80, 0, 0), new Hsv(180, 255, 255));
            //green
            //var image = hsvImage.InRange(new Hsv(45, 0, 0), new Hsv(90, 255, 255));
            var mat = hsvImage.Mat;
            mat.SetTo(new MCvScalar(0), image);
            mat.CopyTo(ret);
            return ret;
        }

        public static Image<Gray, Byte> ConvertImageToBinary(Image<Hsv, Byte> hsvImage)
        {
            var mat = hsvImage.Mat;
            Image<Gray, Byte> gray = mat.ToImage<Gray, Byte>();
            gray = gray.ThresholdBinary(new Gray(10), new Gray(255)).Not();
            gray.Erode(8);
            return gray;
        }
    }
}
