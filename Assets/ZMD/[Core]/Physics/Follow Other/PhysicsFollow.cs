using System;
using UnityEngine;

namespace ZMD
{
    /// Follow a target by applying physical forces.  
    /// Allows for more reliable rotational collision detection than Configurable Joints
    /// and more reliable translational collision detection than Articulation Bodies.
    [Serializable]
    public class PhysicsFollow
    {
        public Transform target;
        
        [Header("Rigidbody Settings")]
        public Rigidbody self;
        public Vector3 centerOfMass;
        public ForceMode forceMode = ForceMode.Acceleration;
        
        [Header("Position Settings")]
        public float maxMoveForce = 2000;
        public float moveForce = 2000;
        public float moveDamper = 50;
        
        [Header("Rotation Settings")]
        public float maxRotationForce = 50;
        public float rotationForce = 50;
        public float rotationDamper = 20;

        [HideInInspector] public ForceMovement position;
        [HideInInspector] public TorqueRotation xRotation;
        [HideInInspector] public TorqueRotation yRotation;

        public void Start() 
        { 
            self.centerOfMass = centerOfMass; 
            xRotation.rb.maxAngularVelocity = maxRotationForce;
            yRotation.rb.maxAngularVelocity = maxRotationForce;
        }
        
        public void FixedUpdate()
        {
            if (!target) return;
            
            xRotation.FixedUpdate();
            yRotation.FixedUpdate();
            
            position.targetPosition = target.position;
            position.FixedUpdate();
        }
            
        public void SetTarget(Transform value)
        {
            target = value;
            xRotation.target = value;
            yRotation.target = value;
        }
        
        public void OnValidate()
        {
            ValidatePosition();
            ValiateRotation(xRotation, Axis.X);
            ValiateRotation(yRotation, Axis.Y);

            if (target) SetTarget(target);
        }
        
        void ValidatePosition()
        {
            position.rb = self;
            position.maxForce = maxMoveForce;
            position.force = moveForce;
            position.damper = moveDamper;
            position.forceMode = forceMode;
        }
        
        void ValiateRotation(TorqueRotation rotation, Axis axis)
        {
            rotation.rb = self;
            rotation.maxForce = maxRotationForce;
            rotation.force = rotationForce;
            rotation.damper = rotationDamper;
            rotation.forceMode = forceMode;
            rotation.followStyle = TorqueRotation.FollowStyle.MatchTarget;
            rotation.alignAxis = axis;
        }
    }
}
