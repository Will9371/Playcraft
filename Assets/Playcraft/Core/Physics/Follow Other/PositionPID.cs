// CREDIT: aldonaletto
// SOURCE: https://answers.unity.com/questions/197225/pid-controller-simulation.html
// Modified by Will Petillo

using System;
using UnityEngine;

namespace Playcraft
{
    /// PID-based system for moving a physics object towards a target position
    [Serializable]
    public class PositionPID
    {
        [Header("References")]
        public Rigidbody rb;

        [Header("Settings")]
        [Tooltip("Rigidbody will rotate towards this angle")]
        public Vector3 targetPosition;
        [Tooltip("More massive objects will require greater force to move at the same velocity.")]
        public float maxForce = 1000; 
        [Tooltip("Proportional gain")]
        public float pGain = 200; 
        [Tooltip("Integral gain")]
        public float iGain = 0.5f; 
        [Tooltip("Differential gain")]
        public float dGain = 10; 
        
        /// Error accumulator
        Vector3 integrator = Vector3.zero; 
        Vector3 lastError = Vector3.zero; 
        Vector3 error;
        Vector3 errorDelta;
        
        /// Actual position
        public Vector3 position { get; private set; }
        /// Current force
        public Vector3 force { get; private set; } 

        public void FixedUpdate()
        {
            position = rb.position;
            error = targetPosition - position; 
            integrator += error * Time.deltaTime; 
            errorDelta = (error - lastError) / Time.deltaTime; 
            lastError = error;
            
            // Calculate the force summing the 3 errors with respective gains
            force = error * pGain + integrator * iGain + errorDelta * dGain;
            
            // Clamp the force to the max value available
            force = Vector3.ClampMagnitude(force, maxForce);
            
            // Apply the force to accelerate the rigidbody
            rb.AddForce(force);
        }
    }
}
