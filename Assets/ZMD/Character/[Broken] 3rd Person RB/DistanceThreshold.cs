using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class DistanceThreshold : MonoBehaviour
    {
        [SerializeField] Vector2 distanceThresholds;
        [SerializeField] UnityEvent OnCloseRange;
        [SerializeField] UnityEvent OnFarRange;
        
        Transform target;
        float distanceToTarget => Vector3.Distance(transform.position, target.position);
        float closeThreshold => isClose ? distanceThresholds.y : distanceThresholds.x; 
        bool isClose;

        public void ReceiveTarget(Transform target) { this.target = target; }
        public void ClearTarget() { target = null; }
        
        void Update()
        {
            if (!target) return;
            isClose = distanceToTarget < closeThreshold;  
                             
            if (isClose) OnCloseRange.Invoke();
            else OnFarRange.Invoke(); 
        }
    }
}
