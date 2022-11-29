// CREDIT: Map-Builder
// SOURCE: https://answers.unity.com/questions/236144/rotate-using-physics.html?page=1&pageSize=5&sort=votes
// Modified by Will Petillo

using System;
using UnityEngine;

namespace ZMD
{
    /// Use torque to rotate an object to match a target rotation.
    /// Use two instances on different axes for full alignment
    [Serializable]
    public class TorqueRotation
    {
        public Rigidbody rb;
        public Transform target;

        public float maxForce = 50;
        public float force = 50;
        public float damper = 20;
        public ForceMode forceMode = ForceMode.Acceleration;
        public enum FollowStyle { None, MatchTarget, AimAtTarget }
        public FollowStyle followStyle = FollowStyle.MatchTarget;
        public Axis alignAxis = Axis.Y;
        
        public float deltaAngle { get; private set; }

        Vector3 referenceAxis;
        Vector3 rotationAxis;
        Vector3 cross;
        
        public void OnValidate()
        {
            rb.maxAngularVelocity = maxForce;
        }
        
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
        
        void StepRotation()
        {
            rotationAxis = VectorMath.GetAxisDirection(rb.transform, alignAxis);
            cross = Vector3.Cross(rotationAxis, referenceAxis);
            deltaAngle = Vector3.Angle(rotationAxis, referenceAxis);
            
            rb.AddTorque(deltaAngle * force * cross, forceMode);
            rb.AddTorque(-rb.angularVelocity * damper, forceMode);
        }
    }
}
