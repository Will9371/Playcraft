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

        bool grabInput;
        public bool isGrabbing;
        public bool isTouchingGrabbable;

        private Vector3 moveCur, movePrev, moveDelta;
        private static int CurrentHandValue;
        
        
        #region State Management
        
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
                movePrev = Vector3.zero;

                isGrabbing = true;
                otherHand.isGrabbing = false;

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

            isGrabbing = false;
            OnRelease.Invoke();
        }
        
        #endregion
        
        
        #region Movement
        
        private void Update()
        {        
            if (isGrabbing && !otherHand.isGrabbing)
                MoveRig();
        }
        
        private void MoveRig()
        {
            moveCur = transform.position;

            // Initialize on starting frame
            if (movePrev == Vector3.zero)
                movePrev = moveCur;
            else
            {
                moveDelta = movePrev - moveCur;
                rb.MovePosition(rig.position + moveDelta);
                moveCur += moveDelta;
                movePrev = moveCur;
            }
        }

        private void ThrowSelf()
        {
            rb.velocity = (moveDelta / Time.deltaTime) * throwStrength;
        }
        
        #endregion
    }
}