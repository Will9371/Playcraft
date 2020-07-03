using System;
using UnityEngine;
using UnityEngine.Events;

public enum TurnType { Straight, Right, Left }

public class AvoidObstacles : MonoBehaviour
{
    [SerializeField] float avoidDistance = 3f;
    [SerializeField] RayData[] rays;
    
    [Serializable] class TurnTypeEvent : UnityEvent<TurnType> { }
    [SerializeField] TurnTypeEvent Output;
            
    private void Start()
    {
        foreach (var ray in rays)
            ray.Start(transform, avoidDistance);
    }
    
    private void Update()
    {
        Output.Invoke(GetTurnType(PollRays()));
    }
    
    private float PollRays()
    {
        float total = 0f;
        
        foreach (var ray in rays)
            total += ray.Update();
            
        return total;
    }
    
    private TurnType GetTurnType(float voteResult)
    {
        if (voteResult == 0f) 
            return TurnType.Straight;
        if (voteResult > 0f)
            return TurnType.Right;
            
        return TurnType.Left;
    }
    
    [Serializable] class RayData
    {
        [SerializeField] Vector3 direction;
        [SerializeField] bool hitIsBlock = true;
        [SerializeField] bool clockwise;
        [SerializeField] bool distanceAttenuate;
        
        Transform self;
        float searchDistance;
        
        bool isBlocked;
        Color debugColor;
        Vector3 localDirection;
        RaycastHit hit;
                
        public void Start(Transform self, float searchDistance)
        {
            this.self = self;
            this.searchDistance = searchDistance;
        }
        
        public float Update()
        {
            SetLocalDirection();
            isBlocked = Physics.Raycast(self.position, localDirection, out hit, searchDistance, -1, QueryTriggerInteraction.Ignore);
            if (!hitIsBlock) isBlocked = !isBlocked;
            debugColor = isBlocked ? Color.red : Color.blue;
            Debug.DrawRay(self.position, localDirection * searchDistance, debugColor);
            return isBlocked ? GetVote() : 0f;
        }
        
        private void SetLocalDirection()
        {
            localDirection = (self.forward * direction.z + 
                             self.right * direction.x + 
                             self.up * direction.y).normalized;
        }
        
        private float GetVote()
        {
            float magnitude = 1f;
        
            if (distanceAttenuate)
            {
                var distance = Vector3.Distance(self.position, hit.point);
                magnitude = (searchDistance - distance) / searchDistance;
            }
            
            return clockwise ? magnitude : -magnitude;
        }
    }
}
