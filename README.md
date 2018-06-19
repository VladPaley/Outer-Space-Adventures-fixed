# Outer Space Adventures
#
To use this app you need to build win standalone
#
Unite assets need to be imported via standart assets 
#
Class LeapMotionApplayer is responcible for holding user input. Each frame it checks the poisition of hand over the
controller. It's also responsible for check for gestures
#
Coordinate is then being normalised to velocity vector wich is scaled by speed. That value is then used to change 
transforms position of astronaut.