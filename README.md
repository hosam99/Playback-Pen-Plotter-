# Playback-Pen-Plotter
The objective of this project is to design and build a 2-axis pen plotter that plays back a user drawn trajectory at will. This allows the user to easily and quickly come up with their own design/trajectories and have them recreated in a short matter of time. This was achieved using a 2-axis gantry with a retractable pen holder . Two 2byj-48 stepper motors were used to control the movement of both axes while a hobby servo motor was used to extend/retract the pen from the drawing board thereby allowing non continuous curves to be drawn. A TI MSP430FR5739 microcontroller evaluation board was used to control the system. The microcontroller is programmed to receive commands via serial communication from a computer through a user interface developed in C# and process them accordingly. The C# application presents the user with a canvas onto which they can draw something much like the windows paint program. Then, following the press of button the machine recreates that trajectory on the drawing platform.


![](/images/Plotter.JPG)

![](images/Plotter%20Demo.gif)
