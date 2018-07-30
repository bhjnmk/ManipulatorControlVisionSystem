using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem.RobotControl
{
    public static class StartStopServo
    {
        public static void ServoOn(SerialPort serialPort)
        {
            string servoOn = "1;1;SRVON" + "\r\n";
            if (serialPort.IsOpen)
            {
                serialPort.Write(servoOn);
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public static void ServoOff(SerialPort serialPort)
        {

            string servoOff = "1;1;SRVOFF" + "\r\n";
            if (serialPort.IsOpen)
            {
                serialPort.Write(servoOff);
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
