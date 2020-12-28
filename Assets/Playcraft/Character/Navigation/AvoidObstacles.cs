using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Navigation
{
    public enum TurnType { Straight, Right, Left }

    public class AvoidObstacles : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float avoidDistance = 3f;
        [SerializeField] RayData[] rays;
        [Serializable] class TurnTypeEvent : UnityEvent<TurnType> { }
        [SerializeField] TurnTypeEvent Output;
        public TurnType turnType;
        #pragma warning restore 0649
                
        void Start()
        {
            foreach (var ray in rays)
                ray.Start(transform, avoidDistance);
        }
        
        void Update() 
        {
            turnType = GetTurnType(PollRays());
            Output.Invoke(turnType); 
        }
        
        void OnDrawGizmos() 
        { 
            foreach (var ray in rays)
                ray.OnDrawGizmos();
        }
        
        float PollRays()
        {
            float total = 0f;
            
            foreach (var ray in rays)
                total += ray.Update();
                
            return total;
        }
        
        TurnType GetTurnType(float voteResult)
        {
            if (voteResult == 0f) 
                return TurnType.Straight;
            if (voteResult > 0f)
                return TurnType.Right;
                
            return TurnType.Left;
        }
        
        [Serializable] class RayData
        {
            #pragma warning disable 0649
            [SerializeField] Vector3 direction;
            [SerializeField] bool hitIsBlock = true;
            [SerializeField] bool clockwise;
            [SerializeField] bool distanceAttenuate;
            #pragma warning restore 0649
            
            Transform self;
            float searchDistance;
            
            bool isBlocked;
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
                return isBlocked ? GetVote() : 0f;
            }
            
            void SetLocalDirection()
            {
                localDirection = (self.forward * direction.z + 
                                 self.right * direction.x + 
                                 self.up * direction.y).normalized;
            }
            
            float GetVote()
            {
                float magnitude = 1f;
            
                if (distanceAttenuate)
                {
                    var distance = Vector3.Distance(self.position, hit.point);
                    magnitude = (searchDistance - distance) / searchDistance;
                }
                
                return clockwise ? magnitude : -magnitude;
            }
            
            public void OnDrawGizmos()
            {
                if (!self) return;
                Gizmos.color = isBlocked ? Color.red : Color.blue;
                Gizmos.DrawRay(self.position, localDirection * searchDistance);
            }
        }
    }
}
