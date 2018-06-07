using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionSystem.SkinDetection
{
    public class ColorSkinDetector : IColorSkinDetector
    {
        public Image<Gray, byte> DetectSkin(Image<Bgr, byte> Img, IColor min, IColor max)
        {

        }
    }
}
