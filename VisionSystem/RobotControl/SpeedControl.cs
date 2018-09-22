using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem.RobotControl
{
    public static class SpeedControl
    {
        public static void SpeedChange(int actualSpeed, SerialPort serialPort)
        {
            string speed = "1;1;OVRD=" + actualSpeed + "\r\n";

            if (serialPort.IsOpen)
            {
                serialPort.Write(speed);
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SpeedChange(int actualSpeed, SerialPort serialPort, TextBox speedTextBox)
        {
            string speed = "1;1;OVRD=" + actualSpeed + "\r\n";

            if (serialPort.IsOpen)
            {
                serialPort.Write(speed);
                speedTextBox.Text = speed;
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
