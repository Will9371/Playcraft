# Playcraft
Diverse collection or reusable systems within the Unity game engine.
Exemplifies a compositional style of programming that is easy to learn and scaleable by default.

INSTALLATION:
For easy installation, double-click the Playcraft Install.UnityPackage with your project open.
For the most up-to-date (but less stable) version, or to contribute, clone the repository.

NON-VR DEVELOPMENT:
If you are NOT using this template for a VR project, be sure to untick the VR folder 
in the import popup window.  If you forget, you can simply delete the VR folder after import.
If you wish to keep the VR folder but want to work on a non-VR scene, 
Go to Project Settings -> XR Plug-in Management and untick "Initialize XR on Startup"
(re-tick this setting when switching back to working in VR).

FEATURES (non-exhaustive):
- 1st person character controller
- 1st person shooter template (extension of 1st person controller)
- 1st person flying prefab (useful for debugging and screen-captures)
- Dialog system with typewriter text
- Additive scene system (WIP)
- General purpose interaction system
- Easy-to-use, UnityEvent-driven input systems for keyboard, mouse
- Object pooling
- Lerp interfaces for position, rotation, scale, and color
- Hex grid drawer that works in editor or at run-time (WIP)
- Dynamically spreading voxel clouds.
- Lots (and lots!) of Quality-of-Life scripts

VR-Specific Features:
- Easy-to-use, UnityEvent-driven input system, including trackpad
- Arc teleport
- 1st person rig setup with teleport, climbing, and grab/throw
- 3rd person animated character controller using trackpad
- Experimental "Monkey Climb" system for climb-based locomotion
- Animated robot hands (open and close)
- Photosphere shader
