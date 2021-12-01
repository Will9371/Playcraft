using UnityEngine;

using System.Collections.Generic;

namespace Playcraft.VR
{
    public class PullMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform puller;
        
        bool moveThisFrame;
        Vector3 priorPosition;

        public void MoveThisFrame() { moveThisFrame = true; }
        
        void Update()
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
            
            if (!StepBlocked())
                rb.MovePosition(position + horizontalStep);
        }
        
        #region Check for barriers
        
        [SerializeField] CapsuleCollider capsule;
        [SerializeField] List<Collider> ignored = new List<Collider>();
        Vector3 capsuleCenter => capsule.transform.position + capsule.center;

        bool StepBlocked()
        {
            var hits = Physics.OverlapSphere(capsuleCenter + horizontalStep, capsule.radius);
            
            foreach (var hit in hits)
                if (!ignored.Contains(hit) && !hit.isTrigger)
                    return true;
                    
            return false;
        }
        
        #endregion
    }
}