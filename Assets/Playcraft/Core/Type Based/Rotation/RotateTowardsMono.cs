using UnityEngine;

namespace Playcraft
{
    public class RotateTowardsMono : MonoBehaviour
    {
        [SerializeField] RotateTowards process;
        
        public float angle => process.angle;
        public void SetDirection(Vector3SO value) { process.direction = value.value; }
        public void SetDirection(Vector3 value) { process.direction = value; }
        
        public void SetTarget(Collider value) { process.SetTarget(value); }
        public void SetTarget(Transform value) { process.SetTarget(value); }
        
        void Update() { process.Update(); }

        public void SetDirectionInstant(Vector3 value) { process.SetDirectionInstant(value); } 
    }
}
