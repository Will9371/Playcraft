using UnityEngine;

namespace Playcraft
{
    public class ApplyForce : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] float multiplier;
        
        public void Input(Vector3 vector) { rb.AddForce(vector * multiplier); }
        
        Vector3 direction = Vector3.forward;
        public void SetDirection(Vector3 value) { direction = value; }
        
        public void Forward() { Input(direction); }
        public void Backward() { Input(-direction); }
    }
}
