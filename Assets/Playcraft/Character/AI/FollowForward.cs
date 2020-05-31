using UnityEngine;

// Move forward when target is within range
// Can follow a target when used in combination with a rotation component
// Dynamic direction is beyond the scope of this component
namespace Playcraft
{
    public class FollowForward : MonoBehaviour
    {
        Transform target;
        public void SetTarget(Transform target) { this.target = target; }
        public void ClearTarget() { target = null; }
        
        #pragma warning disable 0649
        [SerializeField] Vector3 forwardVector = new Vector3(0, 0, 1);
        [SerializeField] RangeCheck followRange;
        [SerializeField] Vector3Event OnMove;
        #pragma warning restore 0649
        
        float targetDistance { get { return Vector3.Distance(transform.position, target.position); } }
        
        bool inRange;
        
        void Update()
        {
            if (!target) return;
            
            inRange = followRange.InRange(targetDistance, inRange);
            
            if (inRange)
                OnMove.Invoke(forwardVector);
        }
    }
}
