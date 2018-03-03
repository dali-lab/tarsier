# tarsier

# Setup

You will need VR-ready Windows PC, with a copy of Unity 5.5 or newer installed on your machine. It is also essential to have an HTC Vive with all hardware installed and the software configured and updated. In the Unity Application, press play to begin. Make sure that HTC Vive and its controllers are powered on and connected!

# Architecture
The project is developed using [Unity](https://unity3d.com/) with models created in Maya.
There are two main scenes: indoor "museum" scene and the outdoor forest scenes.
Any environment the tarsier/player is in can have a Human toggle and a Tarsier toggle. The "toggle" capacity should be the same for any environment the player is in.

* Indoor Museum Scene: the indoor scene consists of a generic room environment with several stations that highlight the biological features of the tariers.
* Forest Scene: The forest consists of an "infinite" environment where in tarier mode the player can be in the trees and engage in tarsier actions:

We plan to use the Steam VR plugin (assets that provide easy development in VR).


# Tech Stack
- Unity
- SteamVR Plugin
- Maya

# Scenes
### Brightness
This scene aims to illustrate the difference in brightness perception between Tarsier and Human vision. It contains a model of a maze imported from Maya, and aims to interactively demonstrate to users how the brightness disparity may affect movement and mobility. The scene currently uses HapticPulse to alert users when they hit into a wall; however, there is no easy way to prevent users from actually accidentally walking through a wall at the moment. In the future, if possible, perhaps it would be better to use a different maze model - such as a series of stacked boxes - that made it easier for the game to prevent users from moving through the objects.
