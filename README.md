# Playcraft
Diverse collection or reusable systems within the Unity game engine.
Exemplifies a compositional style of programming that is easy to learn and scaleable by default.

INSTALLATION:
For easy installation, double-click the Playcraft Install.UnityPackage with your project open.
For the most up-to-date (but less stable) version, or to contribute, clone the repository.

DEPENDENCIES:
- VR folder requires: XR Plugin Management and XR Interaction Toolkit.  Verified with Oculus XR plugin only.
- VFX folder requires: Post Processing.

If you are NOT using this template for a VR project, be sure to untick the VR folder 
in the import popup window.  If you forget, you can simply delete the VR folder after import.  
Same for VFX.

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

VR-SPECIFIC FEATURES:
- Easy-to-use, UnityEvent-driven input system, including trackpad
- Arc teleport
- 1st person rig setup with teleport, climbing.
- Pull-based locomotion (similar to "Gorn").
- 3rd person animated character controller using trackpad (similar to "Moss").
- Experimental "Monkey Climb" system for climb-based locomotion
- Animated robot hands (open and close)
- Photosphere shader

FEEDBACK AND PLANNED UPDATES:
This is a side project for me and I don't plan on turning it into a commercial product.  I mostly add features based on systems developed from other projects.  As such, I am unlikely to add miscellaneous polish or fixes to subsystems I am not actively working on.  If you find any errors with core systems or compilation/build errors, however, 
please let me know and I will fix them as soon as possible.  I welcome external contributions.  If there is anything you want to add to this project, 
please let me know and I can add you as a contributor so you can make pull requests.
