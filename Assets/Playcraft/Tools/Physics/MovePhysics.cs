using UnityEngine;

namespace Playcraft
{
    public class MovePhysics : MonoBehaviour
    {
        public new bool enabled = true;
        
        #pragma warning disable 0649
        [SerializeField] Vector3Event OnMove;
        #pragma warning restore 0649
        
        Rigidbody rb;
        MoveState state;
        float savedSpeed;
                        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            
            if (rb == null)
                Debug.LogError("Attach a Rigidbody!");
        }
        
        public void Enable(bool enabled) { this.enabled = enabled; }
        public void SetState(MoveState state) { this.state = state; }
        
        public void Move(Vector3 direction)
        {
            if (!enabled || state == null)
                return;
        
            var speed = state.disableSpeedControl ? savedSpeed : state.moveSpeed;
            savedSpeed = speed;
            
            var velocity = direction * speed;
            //Debug.Log(velocity + " " + direction + " " + speed);
            var step = velocity * Time.deltaTime;
            var nextPosition = transform.position + transform.TransformVector(step);
            rb.MovePosition(nextPosition);
            OnMove.Invoke(transform.TransformVector(velocity));
        }
    }
}
