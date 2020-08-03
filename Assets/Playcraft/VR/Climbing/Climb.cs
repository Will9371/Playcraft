using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.VR
{
    public class Climb : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float throwStrength;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform rig;
        [SerializeField] Climb otherHand;
        [SerializeField] UnityEvent OnGrab;
        [SerializeField] UnityEvent OnFail;
        [SerializeField] UnityEvent OnRelease;
        #pragma warning restore 0649
        
        public bool isGrabbing;
        public bool isTouchingGrabbable;
        
        bool grabInput;
        Vector3 position, priorPosition, deltaPosition;
        
        GameObject grabbedObject;
        Vector3 grabbedPosition, priorGrabbedPosition, deltaGrabbedPosition;
        
        #region State Management
        
        public void SetTouchingGrabbable(GameObject grabbedObject, bool isGrabbing)
        {
            this.grabbedObject = grabbedObject;
            SetTouchingGrabbable(isGrabbing);
        }
        
        public void SetTouchingGrabbable(bool value) 
        { 
            isTouchingGrabbable = value; 
            
            if (isTouchingGrabbable && grabInput)
                BeginPull(false);
        }
        
        public void RequestGrab()
        {
            grabInput = true;
            BeginPull(true);
        }

        public void BeginPull(bool swappedHand)
        {        
            if (!gameObject.activeSelf || !enabled)
                return;
        
            if (isTouchingGrabbable)
            {
                priorPosition = Vector3.zero;

                isGrabbing = true;
                otherHand.Ungrab();

                rb.useGravity = false;
                rb.velocity = Vector3.zero;

                OnGrab.Invoke();
            }
            else 
                OnFail.Invoke();
        }

        public void Release()
        {
            grabInput = false;
        
            if (otherHand.isGrabbing && otherHand.isTouchingGrabbable)
                otherHand.BeginPull(true);
            else if (isGrabbing)
            {
                rb.useGravity = true;
                ThrowSelf();
            }

            Ungrab();
        }
        
        public void Ungrab()
        {
            isGrabbing = false;
            OnRelease.Invoke();
        }
        
        #endregion
        
        
        #region Movement
        
        private void LateUpdate()
        {        
            if (isGrabbing && !otherHand.isGrabbing)
                MoveRig();
        }
        
        private void MoveRig()
        {
            position = transform.position;
            grabbedPosition = grabbedObject.transform.position;            

            // Initialize on starting frame
            if (priorPosition == Vector3.zero)
            {
                priorPosition = position;
                priorGrabbedPosition = grabbedPosition;
            }
            else
            {
                deltaGrabbedPosition = grabbedPosition - priorGrabbedPosition;
                deltaPosition = priorPosition - position + deltaGrabbedPosition;
                
                //rb.MovePosition(rig.position + deltaPosition);
                rig.Translate(deltaPosition);
                position += deltaPosition;
                
                priorPosition = position;
                priorGrabbedPosition = grabbedPosition;
            }
        }

        private void ThrowSelf()
        {
            rb.velocity = (deltaPosition / Time.deltaTime) * throwStrength;
        }
        
        #endregion
    }
}