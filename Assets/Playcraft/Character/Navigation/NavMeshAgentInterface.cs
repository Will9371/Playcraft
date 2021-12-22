using System;
using UnityEngine;
using UnityEngine.AI;

namespace Playcraft.Navigation
{
    [Serializable]
    public class NavMeshAgentInterface
    {
        public NavMeshAgent agent;
        public Transform target;
        [SerializeField] bool refreshTargetDestination = true;
        public Action<bool> onSetAtTarget;
        
        public void SetTargetAndDestination(Transform value) 
        {
            target = value;
            agent.SetDestination(value.position); 
        }
        
        public void Update()
        {
            if (!target || !agent || !agent.enabled) return;
            
            if (refreshTargetDestination)
                agent.SetDestination(target.position);
                
            atTarget = agentAtTarget;
            
            if (!wasAtTarget && atTarget) 
                onSetAtTarget?.Invoke(true);
            else if (wasAtTarget && !atTarget)
                onSetAtTarget?.Invoke(false);
                
            wasAtTarget = atTarget;
        }
        
        bool agentAtTarget => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && 
                              (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        
        bool atTarget;
        bool wasAtTarget;
    }
}
