# Tarsier Googles
The Tarsier Goggles Project aims to let a user experience the world through the eyes of a tarsier. The experience focuses on different vision adaptions and the everyday life of these unique animals.

## Setup for development

### Prerequisistes

You will need a VR-ready Windows PC, with a copy of Unity 2018.2.12. It is also essential to have an HTC Vive with all hardware installed and the software (including SteamVR) configured and updated.

### Download instructions

Fork or clone the project using git to begin development!

## Setup for play

### Prerequisites 

You will need a VR-ready Windows 10 PC, with an HTC Vive with all its hardware installed and all the software (including SteamVR) configured and updated.

### Download instructions

1. Download the [Tarsier.VR.build](https://github.com/dali-lab/tarsier/releases) to anywhere on your computer.
2. Extract the `Tarsier VR Build` folder from the zip file, and enter that directory. 
3. Ensure that the SteamVR compositor is running (that SteamVR has started up, sensed the hardware, and is currently in the SteamVR Home app)
4. Run the `Tarsier-Goggles` executable file.

__If the `Tarsier-Goggles` executable file and the `Tarsier-Goggles_Data` folder are in different directories, the app may not run.__

### Gameplay

See [here](https://github.com/dali-lab/tarsier/wiki/Gameplay).

## Overview of Developement Implementation

### Vision
The toggled vision is created with components on the SteamVR Camera Eye Game object.

### External Assets
The project relies on the [Colorblind asset](https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/colorblind-effect-76360), the [SteamVR asset](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647) and numerous assets from [VRTK](https://assetstore.unity.com/packages/tools/vrtk-virtual-reality-toolkit-vr-toolkit-64131). Note that, due to the use of VRTK, SteamVR 1.2.3 is the latest version that can be used.

### Post Processing
We use the Post Processing Component on the Camera Eye object for the depth of field and vingette (limited vision) effects

## Tech Stack
- Unity3D
- Maya
