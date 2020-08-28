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
        
        public GameObject grabbedObject;
        Vector3 grabbedPosition, priorGrabbedPosition, deltaGrabbedPosition;
        
        #region State Management
        
        IPosition movingGrabbable;
        
        public void SetTouchingGrabbable(GameObject grabbedObject, bool isGrabbing)
        {
            this.grabbedObject = grabbedObject;
            SetTouchingGrabbable(isGrabbing);
            
            movingGrabbable = grabbedObject.GetComponent<IPosition>();
            if (movingGrabbable != null && isGrabbing) 
                movingGrabbable.position = transform.position;
        }
        
        // * Consider merge into above overload
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
        
        void LateUpdate()
        {        
            if (isGrabbing && !otherHand.isGrabbing)
                MoveRig();
        }
        
        void MoveRig()
        {
            position = transform.position;
            grabbedPosition = movingGrabbable == null ? Vector3.zero : movingGrabbable.position;

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
                
                //rb.MovePosition(rig.position + deltaPosition);    // Causes jittery movement
                rig.Translate(deltaPosition);
                position += deltaPosition;
                
                priorPosition = position;
                priorGrabbedPosition = grabbedPosition;
            }            
        }

        void ThrowSelf()
        {
            rb.velocity = (deltaPosition / Time.deltaTime) * throwStrength;
        }
        
        #endregion
    }
}