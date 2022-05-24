using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD.FPS
{
    public class JumpPhysicsMono : MonoBehaviour
    {
        public JumpPhysics process;
    
        [SerializeField] Rigidbody rb;
        
        [SerializeField] UnityEvent OnJump;
        [SerializeField] UnityEvent OnLand;
        
        bool grounded;
        
        void OnValidate()
        {
            process.rb = rb;
            process.groundCheck.referenceTransform = rb.transform;
            process.OnValidate();
        }
        
        void OnCollisionEnter(Collision other)
        {
            if (process.OnCollisionEnter(other))
                OnLand.Invoke();
        }
        
        public void Jump()
        {
            if (process.Jump())
                OnJump.Invoke();
        }
    }
    
    [Serializable]
    public class JumpPhysics
    {
        public Rigidbody rb;
        public CheckIfValidAngle groundCheck;
        
        public float jumpStrength = 5f;

        [ReadOnly] public bool grounded;

        public bool Jump()
        {
            //Debug.Log($"Jump: {grounded}");
            if (!grounded)
                return false;
        
            grounded = false;
            rb.velocity += jumpStrength * Vector3.up;
            return true;
        }
        
        public bool OnCollisionEnter(Collision other)
        {
            var hasLanded = groundCheck.Input(other);
            //Debug.Log($"Has landed: {hasLanded}");

            if (hasLanded) 
            {
                grounded = true;
            }
            
            return hasLanded;
        }

        public void OnValidate()
        {
            groundCheck.referenceTransform = rb.transform;
            groundCheck.OnValidate();
        }   
    }
}