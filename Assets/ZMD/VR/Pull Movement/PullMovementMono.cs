using System;
using UnityEngine;

namespace ZMD.VR
{
    public class PullMovementMono : MonoBehaviour
    {
        [SerializeField] PullMovement process;
        void Update() { process.Update(); }
        public void MoveThisFrame() { process.moveThisFrame = true; }
    }
    
    [Serializable]
    public class PullMovement
    {
        public Rigidbody rb;
        public Transform puller;
        
        [NonSerialized] public bool moveThisFrame;
        [NonSerialized] public Vector3 priorPosition;
        
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
        bool obstructed;
        Vector3 position => rb.transform.position;
        
        void Pull()
        {
            step = priorPosition - puller.position;
            horizontalStep = new Vector3(step.x, 0f, step.z);
            obstructed = capsuleMovement.StepBlocked(horizontalStep);
            
            if (!obstructed)
                rb.MovePosition(position + horizontalStep);
                
            //Debug.Log($"Step: {horizontalStep}, obstructed: {obstructed}, position: {position}");
        }

        public CheckCapsuleOverlap capsuleMovement;
    }
}