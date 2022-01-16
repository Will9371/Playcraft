// CREDIT: Fraser Hill
// Modified by Will Petillo

// DEPRECATE: causes swords to move through each other 
// (more noticeable when rotation set in this script as well as position)
// Replace with script that moves object gradually, preferably based on physics forces.
// Consider 3-tier abstraction: intention (instant) -> hand (fast physics follow) -> sword (configurable joint)
// Delete this script if swords are the only place it is used

using System;
using UnityEngine;

// OBSOLETE: relies on ConfigurableJoint system
[Serializable]
public class CopyTransform
{
    public Transform self;
    public Rigidbody rb;
    public Transform target;
    
    public bool useRigidbody;
    public bool position;
    public bool rotation;
    public bool scale;
    
    public void Update()
    {
        if (scale) self.localScale = target.localScale;
        
        if (useRigidbody) return;
        
        if (position) self.position = target.position;
        if (rotation) self.rotation = target.rotation;
    }

    public void FixedUpdate()
    {
        if (!useRigidbody || !rb) return;

        if (position) rb.MovePosition(target.position);
        if (rotation) rb.MoveRotation(target.rotation);
    }
}
