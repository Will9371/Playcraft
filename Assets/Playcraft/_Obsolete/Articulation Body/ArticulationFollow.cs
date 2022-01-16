using System;
using UnityEngine;
using Playcraft;

// INCOMPLETE: crashes FPS in VR scene, passes through objects when position changes rapidly.
[Serializable]
public class ArticulationFollow
{
    public ArticulationBody body;
    public Transform target;
    public Rigidbody rb;
    public PositionPID positioner;
    
    //public float speedLimit = 1f;
    Vector3 cachedPosition;

    public void FixedUpdate()
    {
        if (!target) return;
        
        // * Console error when applied to joint, wild behaviour when applied to root.
        // Unable to fix with any known combination of settings
        //body.TeleportRoot(target.position, target.rotation);
        
        // * Cancelled out by root and joint (root moves against RB, joint moves with), no impact on collision
        //rb.MovePosition(target.position);
        //rb.MoveRotation(target.rotation);
        positioner.targetPosition = target.position;
        positioner.FixedUpdate();
        
        body.parentAnchorPosition = cachedPosition; //target.position;
        // * Moves too slow for VR application, changing speed limit has little effect
        //speedLimit > 0 ? Vector3.MoveTowards(body.parentAnchorPosition, target.position, speedLimit * Time.deltaTime) : target.position; 
        //body.parentAnchorRotation = rb.rotation; //target.rotation;
        body.WakeUp();
        cachedPosition = rb.position;
    }
}