using UnityEngine;

namespace Playcraft
{
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] bool outputContinuousOnArrive;
        public void SetTarget(Transform value) { target = value; }
        
        [SerializeField] float speed;
        public void SetSpeed(float value) { speed = value; }
        
        bool wasAtTarget;

        void Update()
        {
            if (!target) return;
            
            var targetDirection = (target.position - transform.position).normalized;
            var step = targetDirection * speed * Time.deltaTime;
            var targetDistance = Vector3.Distance(target.position, transform.position);
            var atTarget = targetDistance <= step.magnitude;
            
            if (!atTarget) transform.Translate(step, Space.World);
            else transform.position = target.position;
            
            if (atTarget && (!wasAtTarget || outputContinuousOnArrive))
            {
                var response = target.GetComponent<TransformRelay>();
                if (response) response.Input(transform);
            }
            wasAtTarget = atTarget;
        }
    }
}
