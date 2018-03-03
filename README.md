# Tarsier Googles
The Tarsier Goggles Project aims to let a user experience the world through the eyes of a tarsier. The game focuses on different vision adaptions and the everyday life of these unique animals

# Setup

You will need VR-ready Windows PC, with a copy of Unity 5.5 or newer installed on your machine. It is also essential to have an HTC Vive with all hardware installed and the software configured and updated. 

In the Unity Application, open the Scenes folder of under Assets. Pull in the forest, brightness, .... scenes. Make sure the Main Room Scene is loaded and press play to begin. Make sure that HTC Vive and its controllers are powered on and connected!

## Gameplay
* Enable/Disable Vision - the radial menu on the right controller enables and disables Tarsier Visions
* Teleport - Click on the left controller button and laser should pop up from the controller. Point the laser to the location you want to teleport to. Green will indicate if teleportation to that location is valid. Let go of the button and you will be teleported there. 
* Pick Up Goggles - when you find a pair of tarsier goggles, move the controller over the object. It should highlight. Hold the trigger and you should be able to pick it up. If you put the goggles on (your head), you will be teleported to a new station.  
* Return to Main Room - Click on the home button. You will be teleported back to the Main Room. 

# Architecture
The project is developed using [Unity](https://unity3d.com/) with models created in Maya.
There are two main scenes: indoor "museum" scene and the outdoor forest scenes.
Any environment the tarsier/player is in can have a Human toggle and a Tarsier toggle. The "toggle" capacity should be the same for any environment the player is in.

* Indoor Museum Scene: the indoor scene consists of a generic room environment with several stations that highlight the biological features of the tariers.
* Forest Scene: The forest consists of an "infinite" environment where in tarier mode the player is in the trees and can jump from tree to tree, but not other locations. For implementation, each tree is surrounded by an invisible quad object with the IncludeTeleport tag. 

## Vision
The toggled vision is created with components on the controller and SteamVR Camera Eye Game objects. 

# Assets
The project relies on a some premade Unity Assets movement and vision features.
## Steam VR Plugin
The SteamVR plugin connects the Unity Project to the HTC Vive. 

## Post Processing
We use the Post Processing Component on the Camera Eye object for the depth of field and vingette (limited vision) effects

## Colorblindness
The Colorblind component provides the red-green colorblindness 

## VRTK
We use the VRTK assets to enable teleportation and vision. In particular we use the Radial Menu Scripts and Dash Teleport. Under the \[VRTK Scripts\] are LeftController and RightController GameObjects. Scripts and modifications should be added to these controllers. 
https://vrtoolkit.readme.io/
