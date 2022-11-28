/****************************************************************************************************

Folder Descriptions:
[Core]: includes files and systems widely depended on by other systems, include in every installation
[Common]: general purpose scripts used in many ZMD subsystems
Prototyping: subset of [Common], trivial logic for rapid UnityEvent-based prototyping
VR: Install for VR projects only, requires modified project settings

Dependency Chain:
Required for all: [Core]
Used in many ZMD systems: [Common]
Used in legacy ZMD systems: Prototyping

Namespaces:
ZMD: DEFAULT, [Core]
* ZMD.Common: [Common]
* ZMD.Common.Proto: [Prototyping]

Questions:
Should Prototyping be part of Common or separate?

****************************************************************************************************/
