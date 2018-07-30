using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem
{
    public static class MovingRobotXYZ
    {
        public enum XYZenum
        {
            Xp,
            Xm,
            Yp,
            Ym,
            Zp,
            Zm
        }

        public static void MoveRobotXYZ(Rectangle rectangleAroundRecognitionGesture, RichTextBox richTextBoxToCheckWhichAxle, SerialPort serialPort)
        {

            if (rectangleAroundRecognitionGesture.X > 425 & rectangleAroundRecognitionGesture.X < 500 & rectangleAroundRecognitionGesture.Y > 150 & rectangleAroundRecognitionGesture.Y < 225)
            {
                richTextBoxToCheckWhichAxle.Text = "X minus";
                SendCommandToMoveXYZ_minus("01", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 0 & rectangleAroundRecognitionGesture.X < 75 & rectangleAroundRecognitionGesture.Y > 150 & rectangleAroundRecognitionGesture.Y < 225)
            {
                richTextBoxToCheckWhichAxle.Text = "X plus";
                SendCommandToMoveXYZ_plus("01", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 225 & rectangleAroundRecognitionGesture.X < 300 & rectangleAroundRecognitionGesture.Y > 300 & rectangleAroundRecognitionGesture.Y < 500)
            {
                richTextBoxToCheckWhichAxle.Text = "y minus";
                SendCommandToMoveXYZ_minus("02", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 225 & rectangleAroundRecognitionGesture.X < 300 & rectangleAroundRecognitionGesture.Y > 0 & rectangleAroundRecognitionGesture.Y < 75)
            {
                richTextBoxToCheckWhichAxle.Text = "y plus";
                SendCommandToMoveXYZ_plus("02", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 425 & rectangleAroundRecognitionGesture.X < 500 & rectangleAroundRecognitionGesture.Y > 300 & rectangleAroundRecognitionGesture.Y < 500)
            {
                richTextBoxToCheckWhichAxle.Text = "z minus";
                SendCommandToMoveXYZ_minus("04", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 0 & rectangleAroundRecognitionGesture.X < 75 & rectangleAroundRecognitionGesture.Y > 0 & rectangleAroundRecognitionGesture.Y < 75)
            {
                richTextBoxToCheckWhichAxle.Text = "z plus";
                SendCommandToMoveXYZ_plus("04", serialPort);
            }
            else
            {
                richTextBoxToCheckWhichAxle.Text = "menu";
            }
        }

        public static void MoveRobotXYZ(XYZenum xyzEnum, SerialPort serialPort)
        {

            switch (xyzEnum)
            {
                case XYZenum.Xm:
                    {
                        SendCommandToMoveXYZ_minus("01", serialPort);
                        break;
                    }
                case XYZenum.Xp:
                    {
                        SendCommandToMoveXYZ_plus("01", serialPort);
                        break;
                    }
                case XYZenum.Ym:
                    {
                        SendCommandToMoveXYZ_minus("02", serialPort);
                        break;
                    }
                case XYZenum.Yp:
                    {
                        SendCommandToMoveXYZ_plus("02", serialPort);
                        break;
                    }
                case XYZenum.Zm:
                    {
                        SendCommandToMoveXYZ_minus("04", serialPort);
                        break;
                    }
                case XYZenum.Zp:
                    {
                        SendCommandToMoveXYZ_plus("04", serialPort);
                        break;
                    }
            }

        }


        public static void SendCommandToMoveXYZ_plus(string moveDirection, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                //serialPort.Write("1;1;JOG01;00;00;" + moveDirection + ";00" + "\r\n");
            }
            else
            {
                //MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SendCommandToMoveXYZ_minus(string moveDirection, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
               // serialPort.Write("1;1;JOG01;00;" + moveDirection + ";00;00" + "\r\n");
            }
            else
            {
               // MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
