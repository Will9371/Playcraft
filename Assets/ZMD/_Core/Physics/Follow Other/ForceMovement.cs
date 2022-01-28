using System;
using UnityEngine;

namespace ZMD
{
    /// Use physical forces to move an object towards a target position
    [Serializable]
    public class ForceMovement
    {
        public Rigidbody rb;
        public Vector3 targetPosition;
        public ForceMode forceMode = ForceMode.Acceleration;
        public float maxForce = 2000; 
        public float force = 2000;
        public float damper = 50;
        
        Vector3 appliedForce;

        public void FixedUpdate()
        {
            appliedForce = (targetPosition - rb.position) * force;
            appliedForce = Vector3.ClampMagnitude(appliedForce, maxForce);
            
            rb.AddForce(appliedForce, forceMode);
            rb.AddForce(-rb.velocity * damper, forceMode);
        }
    }
}
