using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem.RobotControl
{
    public static class MovingRobotJoint
    {
        public enum JointEnum
        {
            J1p,
            J1m,
            J2p,
            J2m,
            J3p,
            J3m,
            J5p,
            J5m,
            J6p,
            J6m,
        }

        public static void MoveRobotJoint(JointEnum jointEnum, SerialPort serialPort)
        {

            switch (jointEnum)
            {
                case JointEnum.J1m:
                    {
                        SendCommandToMoveJoint_minus("01", serialPort);
                        break;
                    }
                case JointEnum.J1p:
                    {
                        SendCommandToMoveJoint_plus("01", serialPort);
                        break;
                    }
                case JointEnum.J2m:
                    {
                        SendCommandToMoveJoint_minus("02", serialPort);
                        break;
                    }
                case JointEnum.J2p:
                    {
                        SendCommandToMoveJoint_plus("02", serialPort);
                        break;
                    }
                case JointEnum.J3m:
                    {
                        SendCommandToMoveJoint_minus("04", serialPort);
                        break;
                    }
                case JointEnum.J3p:
                    {
                        SendCommandToMoveJoint_plus("04", serialPort);
                        break;
                    }
                case JointEnum.J5m:
                    {
                        SendCommandToMoveJoint_minus("10", serialPort);
                        break;
                    }
                case JointEnum.J5p:
                    {
                        SendCommandToMoveJoint_plus("10", serialPort);
                        break;
                    }
                case JointEnum.J6m:
                    {
                        SendCommandToMoveJoint_minus("20", serialPort);
                        break;
                    }
                case JointEnum.J6p:
                    {
                        SendCommandToMoveJoint_plus("20", serialPort);
                        break;
                    }
            }

        }

        public static void SendCommandToMoveJoint_plus(string moveDirection, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;JOG00;00;00;" + moveDirection + ";00" + "\r\n");
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SendCommandToMoveJoint_minus(string moveDirection, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;JOG00;00;" + moveDirection + ";00;00" + "\r\n");
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
