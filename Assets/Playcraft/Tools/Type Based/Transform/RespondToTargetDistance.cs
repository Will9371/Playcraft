using System;
using UnityEngine;
using UnityEngine.Events;

public class RespondToTargetDistance : MonoBehaviour
{
    [SerializeField] Transform target;
    public void SetTarget(Transform value) { target = value; }
    
    [SerializeField] DistanceResponse[] responses;
    
    float distance => Vector3.Distance(transform.position, target.position);
        
    private void Update()
    {
        if (!target) return;
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
        public float respondWhenWithin;
        public UnityEvent response;
    }
}
