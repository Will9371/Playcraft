using UnityEngine;

namespace Playcraft
{
    public class MovePhysics : MonoBehaviour
    {
        public new bool enabled = true;
        
        #pragma warning disable 0649
        [SerializeField] Rigidbody rb;
        [SerializeField] Vector3Event OnMove;
        #pragma warning restore 0649
        
        MoveState state;
        float priorSpeed;
                        
        //private void Awake()
        //{            
        //    if (rb == null)
        //        Debug.LogError("Attach a Rigidbody!");
        //}
        
        public void Enable(bool enabled) { this.enabled = enabled; }
        public void SetState(MoveState state) { this.state = state; }
        
        public void Move(Vector3 direction)
        {
            if (!enabled || state == null)
                return;
        
            var speed = state.disableSpeedControl ? priorSpeed : state.moveSpeed;            
            var horizontal = transform.TransformVector(direction * speed);
            var velocity = new Vector3(horizontal.x, rb.velocity.y, horizontal.z);
            
            rb.velocity = velocity;
            OnMove.Invoke(velocity);
            priorSpeed = speed;
        }
    }
}
