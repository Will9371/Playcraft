using System;
using UnityEngine;

namespace Playcraft
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
        
        [Header("Position Settings")]
        public float maxMoveForce = 2000f;
        public float pGain = 2000f;
        public float iGain = 1f;
        public float dGain = 50f;
        
        [Header("Rotation Settings")]
        public float rotationForce = 100f;
        public float rotationDamper = 20f;
        public ForceMode rotationForceMode = ForceMode.Acceleration;

        [HideInInspector] public PositionPID position;
        [HideInInspector] public TorqueRotation rotation;
        [HideInInspector] public TorqueRotation secondRotation;

        public void Start() { self.centerOfMass = centerOfMass; }

        public void FixedUpdate()
        {
            if (!target) return;
            rotation.FixedUpdate();
            secondRotation.FixedUpdate();
            position.targetPosition = target.position;
            position.FixedUpdate();
        }
            
        public void SetTarget(Transform value)
        {
            target = value;
            rotation.target = value;
            secondRotation.target = value;
        }
        
        public void OnValidate()
        {
            position.rb = self;
            position.maxForce = maxMoveForce;
            position.pGain = pGain;
            position.iGain = iGain;
            position.dGain = dGain;
            
            rotation.rb = self;
            secondRotation.rb = self;
            rotation.force = rotationForce;
            secondRotation.force = rotationForce;
            rotation.damper = rotationDamper;
            secondRotation.damper = rotationDamper;
            rotation.forceMode = rotationForceMode;
            secondRotation.forceMode = rotationForceMode;
            rotation.followStyle = TorqueRotation.FollowStyle.MatchTarget;
            secondRotation.followStyle = TorqueRotation.FollowStyle.MatchTarget;
            rotation.alignAxis = Axis.Y;
            secondRotation.alignAxis = Axis.X;
            
            if (target) SetTarget(target);
        }
    }
}
