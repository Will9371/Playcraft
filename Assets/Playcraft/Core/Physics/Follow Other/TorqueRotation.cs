// CREDIT: Map-Builder
// SOURCE: https://answers.unity.com/questions/236144/rotate-using-physics.html?page=1&pageSize=5&sort=votes
// Modified by Will Petillo

using System;
using UnityEngine;

namespace Playcraft
{
    /// Use torque to rotate an object to match a target rotation.
    /// Use two instances on different axes for full alignment
    [Serializable]
    public class TorqueRotation
    {
        public Rigidbody rb;
        public Transform target;

        public float force = 100;
        public float damper = 20;
        public ForceMode forceMode = ForceMode.Acceleration;
        public enum FollowStyle { None, MatchTarget, AimAtTarget }
        public FollowStyle followStyle = FollowStyle.MatchTarget;
        public Axis alignAxis = Axis.Y;
        
        [HideInInspector] public float deltaAngle;

        Vector3 referenceAxis;
        Vector3 rotationAxis;
        Vector3 cross;
        
        public void FixedUpdate()
        {
            SetReferenceAxis();
            StepRotation();
        }
        
        void SetReferenceAxis()
        {
            if (!target) return;
        
            switch (followStyle)
            {
                case FollowStyle.MatchTarget: referenceAxis = VectorMath.GetAxisDirection(target, alignAxis); break;
                case FollowStyle.AimAtTarget: referenceAxis = (target.position - rb.position).normalized; break;
                case FollowStyle.None: break;
            }
        }
        
        // ERROR: gradually drifts from target on local x-axis 
        // because only alignment on transform.up (Y) axis ensured by this method
        void StepRotation()
        {
            rotationAxis = VectorMath.GetAxisDirection(rb.transform, alignAxis);
            cross = Vector3.Cross(rotationAxis, referenceAxis);
     
            deltaAngle = Vector3.Angle(rotationAxis, referenceAxis);
            deltaAngle = Mathf.Sqrt(deltaAngle);
             
            rb.AddTorque(cross * deltaAngle * force, forceMode);
            rb.AddTorque(-rb.angularVelocity * damper, forceMode);
        }
    }
}
