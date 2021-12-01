using System;
using UnityEngine;

namespace Playcraft.Navigation
{
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
            agent.target = target;
            rotation.SetTarget(target);
        }
        
        public void Start() { agent.onSetAtTarget += SetAtTarget; }
        public void OnDestroy() { agent.onSetAtTarget -= SetAtTarget; }

        public void Update()
        {
            agent.Update();
            if (atTarget) rotation.Update();
        }
        
        public void SetAtTarget(bool atTarget) { this.atTarget = atTarget; }
    }
}
