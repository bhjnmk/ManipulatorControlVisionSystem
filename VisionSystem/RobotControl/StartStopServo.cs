using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionSystem.RobotControl
{
    public static class StartStopServo
    {
        public static void ServoOn(SerialPort serialPort)
        {
            string servoOn = "1;1;SRVON" + "\r\n";
            serialPort.Write(servoOn);
        }

        public static void ServoOff(SerialPort serialPort)
        {
            string servoOff = "1;1;SRVOFF" + "\r\n";
            serialPort.Write(servoOff);
        }
    }
}
