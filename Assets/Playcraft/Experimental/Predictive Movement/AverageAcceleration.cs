using System;
using UnityEngine;

namespace Playcraft
{
    // INCOMPLETE
    [Serializable]
    public class AverageAcceleration
    {
        public AverageVelocity velocity = new AverageVelocity();
        public AverageVelocity acceleration = new AverageVelocity();
        
        public void SetData(AverageVelocitySO data)
        {
            velocity.SetData(data);
            acceleration.SetData(data);
        }

        public void FixedUpdate(Vector3 newPosition) 
        { 
            velocity.FixedUpdate(newPosition);
            
            // ERROR: this is velocity squared, not it's derivative
            //acceleration.FixedUpdate(velocity.projectedPosition);
            
            // OK? Very small numbers
            acceleration.FixedUpdate(velocity.averageDelta * velocity.averageMagnitude);
        }
    }
}