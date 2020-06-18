using UnityEngine;

namespace Playcraft
{
    public class MoveToTargetAccess : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] Transform target;
        
        public void SetSpeed(Transform other) { SetSpeed(other.gameObject); }
        public void SetSpeed(GameObject other)
        {
            var movement = other.GetComponent<MoveToTarget>();
            movement.SetSpeed(speed);        
        }
        
        public void SetSpeedSelf(float value) { speed = value; }
        
        public void SetTarget(Transform other) { SetTarget(other.gameObject); }
        public void SetTarget(GameObject other)
        {
            var movement = other.GetComponent<MoveToTarget>();
            if (movement) movement.SetTarget(target);    
        }
        
        public void SetTargetSelf(Transform value) { target = value; }
        public void SetTargetSelf(GameObject value) { target = value.transform; }
    }
}
