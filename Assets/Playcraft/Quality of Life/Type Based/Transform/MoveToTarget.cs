using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class MoveToTarget : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform target;
        [SerializeField] float speed;
        [SerializeField] float smoothTime = .1f;
        [SerializeField] float stoppingDistance;
        //[SerializeField] MoveEvent Output;
        #pragma warning restore 0649
        
        public Vector3 velocity;

        bool wasAtTarget;
        
        public void SetTarget(Transform value) { target = value; }
        public void SetSpeed(float value) { speed = value; }

        void Update()
        {
            if (!target) return;
            
            var targetDistance = Vector3.Distance(target.position, transform.position);
            var atTarget = targetDistance <= stoppingDistance;
            
            //Output.Invoke(transform.position, target.position, speed);
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime, speed);
            
            if (atTarget && !wasAtTarget)
            {
                var response = target.GetComponent<GameObjectRelay>();
                if (response) response.SetObject(gameObject);
            }
            wasAtTarget = atTarget;
        }
    }
    
    [Serializable] public class MoveEvent : UnityEvent<Vector3, Vector3, float> { }
}
