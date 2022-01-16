using UnityEngine;

namespace Playcraft
{
    public class MoveToTarget : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform target;
        [SerializeField] float speed;
        [SerializeField] float smoothTime = .1f;
        [SerializeField] float stoppingDistance;
        #pragma warning restore 0649
        
        public void SetTarget(Transform value) { target = value; }
        public void SetSpeed(float value) { speed = value; }
        
        Vector3 position => transform.position;
        float targetDistance => Vector3.Distance(target.position, position);
        bool atTarget => targetDistance <= stoppingDistance;
        
        Vector3 velocity;
        bool wasAtTarget;

        void Update()
        {
            if (!target) return;
            transform.position = Vector3.SmoothDamp(position, target.position, ref velocity, smoothTime, speed);
            CheckForTarget();
            wasAtTarget = atTarget;
        }
        
        void CheckForTarget()
        {
            if (!atTarget || wasAtTarget) return;
            var response = target.GetComponent<GameObjectRelay>();
            if (response) response.SetObject(gameObject);
        }
    }    
}
