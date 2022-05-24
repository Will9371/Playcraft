using System;
using UnityEngine;
using UnityEngine.AI;

namespace ZMD.Navigation
{
    /// Extends NavMeshAgent to rotate towards target when within stopping distance
    [Serializable]
    public class RotatingNavMeshAgent
    {
        [ReadOnly] Transform target;
        [HideInInspector] public NavMeshAgentInterface agent;
        [HideInInspector] public RotateTowards rotation;
        [HideInInspector] public KeepDistance keepDistance;
        
        public float moveSpeed = 1f;
        public Vector2 desiredDistance = new Vector2(1f, 1.666f);
        
        [ReadOnly] public bool isStunned;
        bool atTarget;
        
        public void OnValidate() 
        { 
            if (target) SetTarget(target); 
            rotation.forceHorizontal = true;
            
            keepDistance.lockY = true;
            keepDistance.space = Space.World;
            keepDistance.speed = moveSpeed;
            keepDistance.minDistance = desiredDistance.x;

            agent.agent.speed = moveSpeed;
            agent.agent.stoppingDistance = desiredDistance.y;
        }
        
        public void Start() { agent.onSetAtTarget += SetAtTarget; }
        public void OnDestroy() { agent.onSetAtTarget -= SetAtTarget; }

        public void Update()
        {
            if (isStunned) return;
        
            agent.Update();
            
            if (atTarget) 
            {
                rotation.Update();
                keepDistance.Update();
            }
        }
        
        public void SetAtTarget(bool atTarget) { this.atTarget = atTarget; }
        
        public void SetTarget(Transform value)
        {
            target = value;
            agent.target = target;
            rotation.SetTarget(target);
            keepDistance.target = target;
        }
        
        public void SetSelf(Transform self)
        {
            agent.agent = self.GetComponent<NavMeshAgent>();
            rotation.self = self;
            keepDistance.self = self;
        }
    }
}
