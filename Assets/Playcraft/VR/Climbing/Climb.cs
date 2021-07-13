using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.VR
{
    public class Climb : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform rig;
        [SerializeField] Climb otherHand;
        [Header("Settings")]
        [SerializeField] float throwStrength;
        [SerializeField] GrabStyle grabStyle;
        [Header("Output")]
        [SerializeField] UnityEvent OnGrab;
        [SerializeField] UnityEvent OnFail;
        [SerializeField] UnityEvent OnRelease;
       
        enum GrabStyle { Standard, Bubble, Monkey }
        
        [Header("Serialized for debug")]
        public bool isGrabbing;
        public bool isTouchingGrabbable;
        public bool grabInput;
        public GameObject grabbedObject;
        
        Vector3 position, priorPosition, deltaPosition;
        Vector3 grabbedPosition, priorGrabbedPosition, deltaGrabbedPosition;
        
        #region State Management
        
        bool grabOnTouch => grabStyle == GrabStyle.Bubble || grabStyle == GrabStyle.Monkey;
        bool tryGrabOnInput => grabStyle == GrabStyle.Standard;
        bool walkOnHands => grabStyle == GrabStyle.Monkey;
        
        IPosition movingGrabbable;
        
        public void SetTouchingGrabbableTrue(Collider other) { SetTouchingGrabbable(other.gameObject, true); }
        public void SetTouchingGrabbableFalse(Collider other) { SetTouchingGrabbable(other.gameObject, false); }

        public void SetTouchingGrabbable(GameObject grabbedObject, bool isGrabbing)
        {
            this.grabbedObject = grabbedObject;
            SetTouchingGrabbable(isGrabbing);
            
            movingGrabbable = grabbedObject.GetComponent<IPosition>();
            if (movingGrabbable != null && isGrabbing) 
                movingGrabbable.position = transform.position;
        }
        
        public void SetTouchingGrabbable(bool value) 
        { 
            isTouchingGrabbable = value; 
            
            if (grabOnTouch && isTouchingGrabbable && grabInput)
                BeginPull(false);
        }
        
        public void RequestGrab()
        {
            grabInput = true;
            
            if (tryGrabOnInput && isTouchingGrabbable || grabOnTouch)
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
            if (walkOnHands)
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
            if (!walkOnHands)
                grabInput = false;
        
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