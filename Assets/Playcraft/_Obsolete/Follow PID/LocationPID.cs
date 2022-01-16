using System;
using UnityEngine;

namespace Playcraft
{
    /// Combines a rotation and position PID into a single system.
    /// ERROR: erratic results in RotationPID, use PhysicsFollow instead.
    [Serializable]
    public class LocationPID
    {
        [SerializeField] Rigidbody rb;
        public PositionPID position;
        public RotationPID rotation;
        
        public void OnValidate()
        {
            position.rb = rb;
            rotation.rb = rb;
        }
        
        public void FixedUpdate()
        {
            position.FixedUpdate();
            rotation.FixedUpdate();
        }
    }
}
