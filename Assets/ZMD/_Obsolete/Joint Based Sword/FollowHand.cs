using System;
using UnityEngine;

// OBSOLETE: relies on ConfigurableJoint system
[Serializable]
public class FollowHand
{
    public CopyTransform process;
    public void SetHand(Transform value) { process.target = value; }
    public void FixedUpdate() { process.FixedUpdate(); }

    public void SetSelf(Transform value)
    {
        process.self = value;
        process.rb = value.GetComponent<Rigidbody>();
    }
    
    public void OnValidate()
    {
        process.useRigidbody = true;
        process.position = true;
        process.rotation = true;
        process.scale = false;        
    }
}