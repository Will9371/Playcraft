using UnityEngine;

namespace Playcraft
{
    public class MoveToTarget : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform target;
        [SerializeField] bool outputContinuousOnArrive;        
        [SerializeField] float speed;
        #pragma warning restore 0649
        
        bool wasAtTarget;
        
        public void SetTarget(Transform value) { target = value; }
        public void SetSpeed(float value) { speed = value; }

        void Update()
        {
            if (!target) return;
            
            var targetDirection = (target.position - transform.position).normalized;
            //var velocity = targetDirection * speed;
            var step = targetDirection * speed * Time.deltaTime;
            var targetDistance = Vector3.Distance(target.position, transform.position);
            var atTarget = targetDistance <= step.magnitude;
            
            if (!atTarget) transform.Translate(step, Space.World);
            if (atTarget) transform.position = target.position;
            
            if (atTarget && (!wasAtTarget || outputContinuousOnArrive))
            {
                var response = target.GetComponent<TransformRelay>();
                if (response) response.Input(transform);
            }
            wasAtTarget = atTarget;
        }
    }
}
