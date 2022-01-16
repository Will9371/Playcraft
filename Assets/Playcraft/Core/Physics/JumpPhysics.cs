using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class JumpPhysics : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Rigidbody rb;
        
        [SerializeField] float jumpStrength;
        [SerializeField] [Range(0f, 1f)] float jumpHorizontalDamper;
        
        [SerializeField] UnityEvent OnJump;
        [SerializeField] UnityEvent OnLand;
        #pragma warning disable 0649
        
        bool grounded;
        Vector3 velocity;

        private void Awake()
        {
            if (rb == null)
                rb = GetComponent<Rigidbody>();        
            if (rb == null)
                Debug.LogError("Attach a Rigidbody!");
        }
        
        public void Jump()
        {
            //Debug.Log("Jump method reached " + grounded);
            if (!grounded)
                return;
        
            grounded = false;
            rb.velocity += jumpStrength * Vector3.up;
            OnJump.Invoke();
        }
        
        public void Land()
        {
            //Debug.Log("Landing...");
            grounded = true;
            OnLand.Invoke();
        }
    }
}