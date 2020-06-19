using UnityEngine;

namespace Playcraft
{
    public class MoveToTarget : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform target;
        [SerializeField] bool outputContinuousOnArrive;        
        [SerializeField] float speed;
        [SerializeField] float stoppingDistance;
        #pragma warning restore 0649
        
        bool wasAtTarget;
        
        public void SetTarget(Transform value) { target = value; }
        public void SetSpeed(float value) { speed = value; }

        void Update()
        {
            if (!target) return;
            
            var stepDistance = speed * Time.deltaTime;
            var targetDistance = Vector3.Distance(target.position, transform.position);
            var atTarget = targetDistance <= stoppingDistance;
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, stepDistance);
            
            if (atTarget && (!wasAtTarget || outputContinuousOnArrive))
            {
                var response = target.GetComponent<GameObjectRelay>();
                if (response) response.Input(gameObject);
            }
            wasAtTarget = atTarget;
        }
    }
}
