using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem.RobotControl
{
    public static class SendingCommandsToRobot
    {
        public static void SendCommand(string command, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write("1;1;" + command + "\r\n");
                }
                catch
                {

                }
                
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
            
        }
    }
}
