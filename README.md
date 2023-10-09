# SwarmFlowVR - Unity Workspace

## High-Level Description
SwarmFlowVR is a virtual reality user interface for controlling mobile robots. This repository contains the Unity side of SwarmFlowVR, intended for use with the [ROS side](https://github.com/pooleya19/SwarmFlowVR-ROSWS)  of SwarmFlowVR. This repository contains a Unity project, developed in Unity 2021.3.12f1 and exported to a Meta Oculus Quest virtual reality headset. SwarmFlowVR heavily relies on a ROS network for communication. Our implementation was designed to control ROSbot 2.0 Pro robots with pose feedback from an OptiTrack motion capture camera array.

## Setting Up SwarmFlowVR on your Quest
1. Verify that  the Oculus Quest in [development mode](https://developer.oculus.com/documentation/unity/unity-enable-device/).
2. Clone this repository.
3. In the repository, use Unity 2021.3.12f1 to open the project named "UnityProject".
4. In Unity project, in File > Build Settings, under Platform, click Android and click Switch Platform.
5. Verify headset is connected and listed under the Run Device list (click refresh if needed).
6. Click Build and Run to compile app to quest.

## Disclaimer
If you have a history of photosensitivity, epilepsy, or experiencing adverse effects from virtual reality (such as motion sickness, nausea, or vomiting), please be careful when using SwarmFlowVR. Consider sitting down in a safe, unobstructed area. During our testing, the headset could be taken off at ANY time, with no consequences to the ROS network.

## User Guide
During operation of SwarmFlowVR, the right controller is primarily used to issue commands, while the left controller is used to position a virtual menu, used to specify various settings and functions.

SwarmFlowVR allows you to command robots to specific positions around the virtual environment, and the system will move the physical robots to mimic the virtual robots. Each robot can be issued individual commands. To select a robot, point to it and click the ‘index trigger’. Then, to select a waypoint to move towards, point to a position on the floor and click the ‘A button’.

You can change how and when the robots move by selecting one of the four input schemes, named ‘Instant Click’, ‘Instant Hold’, ‘Single Buffer’, and ‘Multiple Buffer’, selected also using the ‘index trigger’. In ‘Instant Click’, a single waypoint is selected when you press the ‘A button’. In ‘Instant Hold’, the waypoint is continuously moved as you hold down the ‘A button’. For ‘Instant Click’ and ‘Instant Hold’, the robots will move as soon as you select a point, but for ‘Single Buffer’ and ‘Multiple Buffer’, the robots won’t start moving until you press play. In ‘Single Buffer’, you can only buffer one single waypoint, but in ‘Multiple Buffer’, you can select many waypoints for the robot to move after in sequence. 

There are multiple ways to move around in SwarmFlowVR. You can either physically move around, you can use the joysticks to move, OR you can transform the virtual world by reaching out with your hands and pressing the ‘hand triggers’ to grab the virtual world, allowing you to translate, rotate, or scale the virtual world. To use virtual world transformation, reach out with *both* hands, press both 'hand triggers', and move your hands together/apart/up/down/around.

Additionally, at the bottom of the virtual menu, there are two buttons, labeled "Load ROS" and "Load Sim". These buttons, selected with the 'index trigger', will reload SwarmFlowVR in ROS-mode or simulation-mode, respectively. When in ROS-mode, SwarmFlowVR will attempt to connect to the ROS network, update the virtual robots from the real-world robot poses, and publish target waypoint information. When in simulation-mode, SwarmFlowVR will simulate the poses, control, and motions of multiple ROSbots, without connecting to any ROS network.
