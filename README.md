# Outer Space Adventures
#
To use this app you need to build win standalone
You also will need to download leap SDK
#
Unite assets need to be imported via standart assets 
#
Class LeapMotionApplayer is responcible for holding user input. Each frame it checks the poisition of hand over the
controller. It's also responsible for check for gestures
#
Coordinate is then being normalised to velocity vector wich is scaled by speed. That value is then used to change 
transforms position of astronaut.

#
To reicive precise coordinates of hand over controller you'll need to use method LeapMotion.PalmOnViewportXZ()
LeapMotion  class is encapsulating leap motion internal data and present convinient interface on which we will rely
Method PalmOnViewportXZ() returnv Vector2, of x and y coordinates of hand. Thouse are positioned with zero values in top 
left corner of active area of leap motion controller. Thouse are then normasied by formula: 0.5f-x(y)
By this we will reicive 0,0 at the middle of the zone, and values from -0.5-0.5 in each axis. 
Thouse values are multiplayed by 2 to make a unit vector, which we use as our velocity direction.
So formula to calculate current velocity will be: speed * velocity
This velocity is constanlty applayed to character.
#
To reicive gestures values we can call method  LeapMotion.ExtendedFingers(). This will return int value 
of extendesd fingers count. Thouse are binded to some ingame actions which occur immidiatly on gesture is recognized
