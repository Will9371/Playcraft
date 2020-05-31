using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class DistanceThreshold : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector2 distanceThresholds;
        [SerializeField] UnityEvent OnCloseRange;
        [SerializeField] UnityEvent OnFarRange;
        #pragma warning restore 0649
        
        Transform target;
        float distanceToTarget { get { return Vector3.Distance(transform.position, target.position); } }
        float closeThreshold { get { return isClose ? distanceThresholds.y : distanceThresholds.x; } }
        bool isClose;

        public void ReceiveTarget(Transform target) { this.target = target; }
        public void ClearTarget() { target = null; }
        
        private void Update()
        {
            if (!target) return;
            isClose = distanceToTarget < closeThreshold;  
                             
            if (isClose) OnCloseRange.Invoke();
            else OnFarRange.Invoke(); 
        }
    }
}
