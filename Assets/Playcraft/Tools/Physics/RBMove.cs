using UnityEngine;

namespace Playcraft
{
    public class RBMove : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Rigidbody rb;
        [SerializeField] float speed = 1f;
        #pragma warning restore 0649
        
        public void SetSpeed(float value) { speed = value; }
        
        public void MoveLocal(Vector3 input)
        {
            if (!enabled) return;
            rb.velocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed);
        }

        public void Move(Vector3 input)
        {
            if (!enabled) return;
            MoveLocal(transform.TransformVector(input));
        }
    }
}
