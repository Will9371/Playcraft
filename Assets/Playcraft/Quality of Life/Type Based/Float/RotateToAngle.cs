using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class RotateToAngle
    {
        [SerializeField] float desiredAngle; 
        [SerializeField] float turnSpeed = 360f;

        float angle;
            
        bool hasArrived = true;
        bool arrived => angle == desiredAngle;
            
        public void SetRotationSpeed(float value) { turnSpeed = value; }
            
        public void SetDesiredAngle(float value) 
        {
            hasArrived = false; 
            desiredAngle = value; 
        }

        public (float angle, bool hasArrived) Tick(float timeStep)
        {    
            angle = VectorMath.RotateToAngle(angle, desiredAngle, timeStep * turnSpeed);
            hasArrived = arrived;
            return (angle, !hasArrived && arrived);
        }       
    }
}
