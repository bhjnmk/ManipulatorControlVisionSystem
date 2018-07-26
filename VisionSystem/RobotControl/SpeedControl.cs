using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionSystem.RobotControl
{
    public static class SpeedControl
    {
        public static void SpeedChange(int actualSpeed, SerialPort serialPort)
        {
            string speed = "1;1;OVRD=" + actualSpeed + "\r\n";
            serialPort.Write(speed);
        }
    }
}
