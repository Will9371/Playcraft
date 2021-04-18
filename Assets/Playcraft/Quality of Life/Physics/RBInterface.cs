﻿using UnityEngine;

namespace Playcraft
{
    public interface IAddForce { void AddForce(Vector3 force, ForceMode mode = ForceMode.Force); }

    public class RBInterface : MonoBehaviour, IAddForce
    {
        [SerializeField] Rigidbody rb;
        
        void Awake()
        {
            if (!rb) 
            {
                rb = GetComponent<Rigidbody>();
                if (!rb) Debug.LogError("Attach or assign a Rigidbody to " + gameObject.name);
            }
            
            RecordPhysicsState();
        }
        
        bool priorGravity;
        bool priorKinematic;
        
        void RecordPhysicsState()
        {
            priorGravity = rb.useGravity;
            priorKinematic = rb.isKinematic;
        }

        public void SetPhysicsActive(bool active)
        {
            RecordPhysicsState();
            
            rb.useGravity = active;
            rb.isKinematic = !active;
            
            if (!active)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        
        public void ResetPhysics()
        {
            rb.useGravity = priorGravity;
            rb.isKinematic = priorKinematic;
        }
        
        public void SetVelocity(Vector3 velocity, Vector3 angularVelocity)
        {
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;
        }
        
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
        {
            rb.AddForce(force, mode);
        }
    }
}
