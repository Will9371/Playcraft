using System;
using UnityEngine;

namespace Playcraft.VR
{
    [Serializable]
    public class PullMovement
    {
        public Rigidbody rb;
        public Transform puller;
        
        [HideInInspector] 
        public bool moveThisFrame;
        Vector3 priorPosition;
        
        public void Update()
        {
            if (moveThisFrame)
                Pull();
            
            priorPosition = puller.position;
            if (moveThisFrame) priorPosition += horizontalStep;
            
            moveThisFrame = false;
        }
        
        Vector3 step;
        Vector3 horizontalStep;
        Vector3 position => rb.transform.position;
        
        void Pull()
        {
            step = priorPosition - puller.position;
            horizontalStep = new Vector3(step.x, 0f, step.z);
            
            if (!capsuleMovement.StepBlocked(horizontalStep))
                rb.MovePosition(position + horizontalStep);
        }
        
        public CheckCapsuleOverlap capsuleMovement;
    }
}
