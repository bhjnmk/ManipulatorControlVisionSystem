# VisionSystem and 

```
Copyright (C) 2018 by Magdalena Kuzmicz
Version: 1.0
Technologies: C#, OpenCV/EmguCV, WindowsForms
```

## About the project

Project is a part of my master thesis. The purpose of it was to create a basic control system based on gesture recognition, which will enable control of the manipulator.
I created an intuitive application that allows the user to control the manipulator in the JOINT and XYZ axis system. The manipulator uses protocol R3 and cmd are send by COM port.

The application allows you to control the robot manually by sending raw commands or by using buttons to move manipulator in axis XYZ or JOINT.

## How to use

The application was tested with robot Mitsubishi RV-2AJ. Computer was connected with control unit by RS232C – USB.
To use the application and check how it works you must connect your computer to manipulator which uses R3 protocol to comunicate.

### Parameters

To connect with manipualtor check proper COM port in main window and set: baud rate: 9600, parity: Even, data bits: 8, stop bits: Two.

## App view

### Connection

![alt text](https://github.com/bhjnmk/VisionSystem/blob/master/Img/Connection.PNG?raw=true)

### Manual control XYZ

![alt text](https://github.com/bhjnmk/VisionSystem/blob/master/Img/XYZ.PNG?raw=true)

### Manual control JOINT

![alt text](https://github.com/bhjnmk/VisionSystem/blob/master/Img/JOINT.PNG?raw=true)

### Manual control CMD

![alt text](https://github.com/bhjnmk/VisionSystem/blob/master/Img/CMD.PNG?raw=true)



