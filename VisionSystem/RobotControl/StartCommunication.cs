using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem.RobotControl
{
    static class StartCommunication
    {
        public static void StartComunicationWithRobot(SerialPort serialPort)
        {
            try
            {
                serialPort.Open();
                MessageBox.Show("Połączono z " + serialPort.PortName);

                serialPort.Write("1;1;OPEN=melfa" + "\r\n");   //otwarcie połączenia z robotem
                serialPort.Write("1;1;CNTLON" + "\r\n");       //zezwolenie na sterowanie z urządzenia zewnętrznego
                serialPort.Write("1;1;SRVOFF" + "\r\n");       //wyłączenie serwomechanizmów
                serialPort.Write("1;1;OVRD=0" + "\r\n");       //ustawienie prędkości 0
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia.\nSprawdź ustawienia lub przewód komunikacyjny.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.ToString(), "Error info", MessageBoxButtons.OK);
            }
        }
    }
}
