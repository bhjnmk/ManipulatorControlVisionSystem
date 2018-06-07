using Emgu.CV;
using Emgu.CV.Structure;

namespace VisionSystem.SkinDetection
{
    public interface IColorSkinDetector
    {
        Image<Gray, byte> DetectSkin(Image<Bgr, byte> Img, IColor min, IColor max);
    }
}