# Tarsier Googles
The Tarsier Goggles Project aims to let a user experience the world through the eyes of a tarsier. The experience focuses on different vision adaptions and the everyday life of these unique animals.

# Setup for development

You will need VR-ready Windows PC, with a copy of Unity 2018.2.12. It is also essential to have an HTC Vive with all hardware installed and the software configured and updated. 

Open the top level project folder in Unity, and run the "Start" Scerne to begin the experience.

# Setup for play

You will need a VR-ready Windows 10 PC, with an HTC Vive with all its hardware installed and all the software configured and updated.
You can then run the built experience, found in the Builds folder. 

## Gameplay
* Change which animal's eyes you see through- Click on different parts of the Right Trackpad to change from Human to Tarsier vision and back.
* Teleport - Click on the right trigger button and a laser pointer will appear from the controller, signalling where the player will teleport to when the trigger is released. The pointer will be blue if the location is valid, and yellow if the location is not.
* Changing Scenes - Click on the left trigger button to bring up a menu of different scenes to choose from. Then, clicking on the top and bottom of the left trackpad will change the selected option, and clicking on the left trigger again will move you to the chosen scene (or escape the menu, if "Cancel" is chosen).

# Architecture
The project is developed using [Unity](https://unity3d.com/) with models created in Maya.
There are four main scenes, with different controls enabled.

* Landing Scene: This is the scene that the player will be first placed in, and where they will choose to have their experience be guided or unguided.
* Crazy Rods Scene: The scene consists of many rods at different rotations and placements relative to the player. There are differently coloured lights throughout the scene. Teleportation is disabled here.
* Maze Scene: The scene consists of a small maze that the player can teleport around. The maze has very dim, differently coloured lights around the scene.
* Forest Scene: The forest consists of an "infinite" environment where the player is in the trees and can jump from tree to tree (or vine). 

## Vision
The toggled vision is created with components on the SteamVR Camera Eye Game object.

# Assets
The project relies on a some premade Unity Assets movement and vision features.

## Steam VR Plugin
The SteamVR plugin connects the Unity Project to the HTC Vive. 

## Post Processing
We use the Post Processing Component on the Camera Eye object for the depth of field and vingette (limited vision) effects

## Colorblindness
The Colorblind component provides the red-green colorblindness that Tarsiers have.

## VRTK
We use the VRTK assets to enable teleportation and vision. In particular we use Radial Menu Scripts, Teleportation Scripts, and Panel Menu Scripts.

# Tech Stack
- Unity
- SteamVR
- Maya

# Modes of Play
When opened, the experience allows a player to choose to have their experience be "Guided" or "Unguided"

## Guided
In this version, recommended for first-time players, the player will have controls taught to them incrementally, and have those controls locked until that time. The player will be instantly teleported to the Crazy Rod Room, where they will be taught about switching between Tarsier and Human Vision. When the player has spent enough time in there, they will be prompted to teleport to the Maze, where they will also be introduced to Teleportation. After a certain amount of time and movement in the maze, the player will be prompted to move to the Forest, where they will be taught how to switch between scenes, and the guided part of the experience will end.

## Unguided
In this version, the player will have all controls immediately enabled, and will be given no direction to switch between scenes.
