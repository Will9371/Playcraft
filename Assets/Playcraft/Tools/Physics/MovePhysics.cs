using UnityEngine;

// REFACTOR 3rd person character (and AI) to eliminate reliance on MoveState
// following example in 1st person character, then remove variable from this class
namespace Playcraft
{
    public class MovePhysics : MonoBehaviour
    {
        public new bool enabled = true;
        
        #pragma warning disable 0649
        [SerializeField] Rigidbody rb;
        [SerializeField] Vector3Event OnMove;
        [SerializeField] MoveState state;
        [SerializeField] float baseSpeed;
        #pragma warning restore 0649
        
        float speed;
        float priorSpeed;
        
        float speedMultiplier = 1f;
        public void SetSpeedMultiplier(float value) { speedMultiplier = value; }
                        
        private void Awake()
        {            
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
                
                if (rb == null)
                    Debug.LogError("Attach or assign a Rigidbody to " + gameObject.name);
            }
        }
        
        public void Enable(bool enabled) { this.enabled = enabled; }
        public void SetState(MoveState state) { this.state = state; }
        
        public void Move(Vector3 direction)
        {
            if (!enabled) return;
        
            if (state != null)
                speed = state.disableSpeedControl ? priorSpeed : state.moveSpeed; 
            else
                speed = baseSpeed * speedMultiplier;
                  
            var horizontal = transform.TransformVector(direction * speed);
            var velocity = new Vector3(horizontal.x, rb.velocity.y, horizontal.z);
            
            rb.velocity = velocity;
            OnMove.Invoke(velocity);
            priorSpeed = speed;
        }
    }
}
