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
        public static void MoveRobotXYZ(Rectangle rectangleAroundRecognitionGesture, int oneThirdOfTheCamPic, RichTextBox richTextBoxToCheckWhichAxle, SerialPort serialPort)
        {
            if (rectangleAroundRecognitionGesture.X > 2 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.X < 3 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y > oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y < 2 * oneThirdOfTheCamPic)
            {
                richTextBoxToCheckWhichAxle.Text = "X minus";
                SendCommandToMoveXYZ_minus("01", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 0 & rectangleAroundRecognitionGesture.X < oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y > oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y < 2 * oneThirdOfTheCamPic)
            {
                richTextBoxToCheckWhichAxle.Text = "X plus";
                SendCommandToMoveXYZ_plus("01", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.X < 2 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y > 2 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y < 3 * oneThirdOfTheCamPic)
            {
                richTextBoxToCheckWhichAxle.Text = "y minus";
                SendCommandToMoveXYZ_minus("02", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.X < 2 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y > 0 & rectangleAroundRecognitionGesture.Y < oneThirdOfTheCamPic)
            {
                richTextBoxToCheckWhichAxle.Text = "y plus";
                SendCommandToMoveXYZ_plus("02", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 2 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.X < 3 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y > 2 * oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y < 3 * oneThirdOfTheCamPic)
            {
                richTextBoxToCheckWhichAxle.Text = "z minus";
                SendCommandToMoveXYZ_minus("04", serialPort);
            }
            else if (rectangleAroundRecognitionGesture.X > 0 & rectangleAroundRecognitionGesture.X < oneThirdOfTheCamPic & rectangleAroundRecognitionGesture.Y > 0 & rectangleAroundRecognitionGesture.Y < oneThirdOfTheCamPic)
            {
                richTextBoxToCheckWhichAxle.Text = "z plus";
                SendCommandToMoveXYZ_plus("04", serialPort);
            }
            else
            {
                richTextBoxToCheckWhichAxle.Text = "menu";
            }
        }

        public static void SendCommandToMoveXYZ_plus(string moveDirection, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;JOG01;00;00;" + moveDirection + ";00" + "\r\n");
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SendCommandToMoveXYZ_minus(string moveDirection, SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("1;1;JOG01;00;" + moveDirection + ";00;00" + "\r\n");
            }
            else
            {
                MessageBox.Show("Błąd komunikacji z robotem", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
