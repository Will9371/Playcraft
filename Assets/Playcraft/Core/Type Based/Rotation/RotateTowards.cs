using System;
using UnityEngine;

namespace Playcraft
{
    // Continuously rotate towards a target transform
    [Serializable]
    public class RotateTowards
    {
        [Header("Settings")]
        public Transform self;
        public float speed = 120;
        public bool forceHorizontal;
            
        [Header("Debug")]
        public Transform target;
        public Vector3 direction;

        /// Angle in degrees between forward vector from self and desired direction
        public float angle => Vector3.Angle(self.forward, facingDirection);
        Quaternion lookRotation => Quaternion.LookRotation(facingDirection);
        Vector3 horizontalDirection => new Vector3(direction.x, 0, direction.z);
        Vector3 facingDirection => forceHorizontal ? horizontalDirection : direction;
        float step => speed * Time.deltaTime;
        
        public void SetTarget(Collider value) { target = value ? value.transform : null; }
        public void SetTarget(Transform value) { target = value; }
        
        public void Update()
        {
            // Face target if applicable
            if (target) direction = (target.position - self.position).normalized;
            
            // Default direction is forward
            if (direction == Vector3.zero) direction = self.forward;
                
            // Turn incrementally towards desired direction
            self.rotation = Quaternion.RotateTowards(self.rotation, lookRotation, step); 
        }

        public void SetDirectionInstant(Vector3 value) 
        {
            direction = value; 
            self.rotation = Quaternion.LookRotation(value); 
        }
    }
}
