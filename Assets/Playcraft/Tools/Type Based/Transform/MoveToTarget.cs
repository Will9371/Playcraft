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
        [SerializeField] float stoppingDistance;
        [SerializeField] MoveEvent Output;
        #pragma warning restore 0649
        
        bool wasAtTarget;
        
        public void SetTarget(Transform value) { target = value; }
        public void SetSpeed(float value) { speed = value; }


        void Update()
        {
            if (!target) return;
            
            var targetDistance = Vector3.Distance(target.position, transform.position);
            var atTarget = targetDistance <= stoppingDistance;
            
            Output.Invoke(transform.position, target.position, speed);
            
            if (atTarget && !wasAtTarget)
            {
                var response = target.GetComponent<GameObjectRelay>();
                if (response) response.Input(gameObject);
            }
            wasAtTarget = atTarget;
        }
    }
    
    [Serializable] public class MoveEvent : UnityEvent<Vector3, Vector3, float> { }
}
