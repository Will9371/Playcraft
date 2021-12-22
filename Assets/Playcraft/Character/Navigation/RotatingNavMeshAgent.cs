using System;
using UnityEngine;
using UnityEngine.AI;

namespace Playcraft.Navigation
{
    /// Extends NavMeshAgent to rotate towards target when within stopping distance
    [Serializable]
    public class RotatingNavMeshAgent
    {
        public Transform target;
        public NavMeshAgentInterface agent;
        public RotateTowards rotation;
        
        public Action<bool> onSetAtTarget 
        {
            get => agent.onSetAtTarget;
            set => agent.onSetAtTarget = value;
        }

        bool atTarget;
        
        public void OnValidate() 
        { 
            if (target) SetTarget(target); 
            rotation.forceHorizontal = true;
        }
        
        public void Start() { agent.onSetAtTarget += SetAtTarget; }
        public void OnDestroy() { agent.onSetAtTarget -= SetAtTarget; }

        public void Update()
        {
            agent.Update();
            if (atTarget) rotation.Update();
        }
        
        public void SetAtTarget(bool atTarget) { this.atTarget = atTarget; }
        
        public void SetTarget(Transform value)
        {
            target = value;
            agent.target = target;
            rotation.SetTarget(target);
        }
        
        public void SetSelf(Transform self)
        {
            agent.agent = self.GetComponent<NavMeshAgent>();
            rotation.self = self;
        }
    }
}
