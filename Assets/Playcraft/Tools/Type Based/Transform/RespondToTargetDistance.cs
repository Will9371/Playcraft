using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RespondToTargetDistance : MonoBehaviour
    {
        [SerializeField] Transform target;
        public void SetTarget(Transform value) { target = value; }
        public void SetTargetPosition(RaycastHit value) { SetTargetPosition(value.point); }
        public void SetTargetPosition(Vector3 value) { targetPosition = value; }
        public void ClearTargetPosition() { targetPosition = null; }
        Vector3? targetPosition;
        
        [SerializeField] DistanceResponse[] responses = default;
        
        float distance => Vector3.Distance(transform.position, (Vector3)destination);        
        Vector3? destination => target ? target.position : targetPosition;
            
        private void Update()
        {
            if (!target && targetPosition == null) return;
            GetResponse(distance).response.Invoke();
        }
        
        private DistanceResponse GetResponse(float distance)
        {
            foreach (var response in responses)
                if (distance <= response.respondWhenWithin)
                    return response;
                    
            return responses[responses.Length - 1];
        }
        
        [Serializable] struct DistanceResponse
        {
            #pragma warning disable 0649
            public float respondWhenWithin;
            public UnityEvent response;
            #pragma warning restore 0649
        }
    }
}
