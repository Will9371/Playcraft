// CREDIT: Map-Builder
// SOURCE: https://answers.unity.com/questions/236144/rotate-using-physics.html?page=1&pageSize=5&sort=votes
// Modified by Will Petillo

using System;
using UnityEngine;

namespace Playcraft
{
    /// Use torque to rotate an object to match a target rotation.
    /// WARNING: does not account for y-axis.  Set Rigidbody.freezeRotation.y to True to prevent unwanted roll.
    [Serializable]
    public class TorqueRotation
    {
        public Rigidbody rb;
        [SerializeField] Vector3 direction = Vector3.up;
        [SerializeField] float force = 100;
        [SerializeField] float damper = 20;
        [SerializeField] ForceMode forceMode = ForceMode.Acceleration;
        
        enum FollowStyle { None, MatchTarget, AimAtTarget }
        [SerializeField] FollowStyle followStyle = FollowStyle.MatchTarget;
        public Transform target;
        
        Vector3 rotationAxis;
        Vector3 cross;
        float deltaAngle;

        public void FixedUpdate()
        {
            SetFollow();
            StepRotation();
        }
        
        void SetFollow()
        {
            if (!target) return;
        
            switch (followStyle)
            {
                case FollowStyle.MatchTarget: MatchOther(target); break;
                case FollowStyle.AimAtTarget: AimAtPoint(target); break;
                case FollowStyle.None: break;
            }
        }
        
        void StepRotation()
        {
            rotationAxis = rb.transform.up;
            cross = Vector3.Cross(rotationAxis, direction);
     
            deltaAngle = Vector3.Angle(rotationAxis, direction);
            deltaAngle = Mathf.Sqrt(deltaAngle);
             
            rb.AddTorque(cross * deltaAngle * force, forceMode);
            rb.AddTorque(-rb.angularVelocity * damper, forceMode);
        }
        
        void AimAtPoint(Transform other) { AimAtPoint(other.position); }
        void AimAtPoint(Vector3 point) { direction = point - rb.position; }
        
        void MatchOther(Transform other) { direction = other.up; }
    }
}
