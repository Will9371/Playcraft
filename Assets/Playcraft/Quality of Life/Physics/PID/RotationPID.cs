// CREDIT: aldonaletto
// SOURCE: https://answers.unity.com/questions/197225/pid-controller-simulation.html
// Modified by Will Petillo

using System;
using UnityEngine;

namespace Playcraft
{
    /// PID-based system for rotating a physics object towards a target rotation
    [Serializable]
    public class RotationPID
    {
        [Header("References")]
        public Rigidbody rb;
        
        [Header("Settings")]
        [Tooltip("Rigidbody will rotate towards this angle")]
        public Vector3 targetAngle;
        [Tooltip("Max acceleration in degrees/second^2")]
        public float maxAcceleration = 2000;
        [Tooltip("Max angular speed in degrees/second")]
        public float maxAngularSpeed = 500;
        [Tooltip("Proportional gain")]                        
        public float pGain = 1000; 
        [Tooltip("Differential gain")]
        public float dGain = 100;  
        
        const float degreesInCircle = 360;
        
        public Vector3 angle { get; private set; }
        public Vector3 acceleration { get; private set; }
        public Vector3 angularSpeed { get; private set; }
        
        Vector3 error;
        Vector3 lastError;
        Vector3 errorDelta;

        public void FixedUpdate()
        {
            error = targetAngle - angle;
            errorDelta = (error - lastError) / Time.deltaTime; 
            lastError = error;
            
            acceleration = error * pGain + errorDelta * dGain;
            acceleration = ClampVector(acceleration, maxAcceleration);

            angularSpeed += acceleration * Time.deltaTime;
            angularSpeed = ClampVector(angularSpeed, maxAngularSpeed);

            angle += angularSpeed * Time.deltaTime;
            
            // OK in isolation, but still has problem of moving through colliders
            rb.rotation = Quaternion.Euler(angle.x % degreesInCircle, angle.y % degreesInCircle, angle.z % degreesInCircle);
        }

        Vector3 ClampVector(Vector3 vector, float max)
        {
            return new Vector3(Mathf.Clamp(vector.x, -max, max),
                Mathf.Clamp(vector.y, -max, max),
                Mathf.Clamp(vector.z, -max, max));
        }
    }
}