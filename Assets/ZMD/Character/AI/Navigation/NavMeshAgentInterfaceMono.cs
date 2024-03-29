using System;
using UnityEngine;
using UnityEngine.AI;

namespace ZMD.Navigation
{
    public class NavMeshAgentInterfaceMono : MonoBehaviour
    {
        [SerializeField] NavMeshAgentInterface process;
        public void SetTarget(Transform value) { process.SetTargetAndDestination(value); }
        void Update() { process.Update(); }
        
        // TBD: consider adding UnityEvent hook to process.OnReachTarget
    }
    
    [Serializable]
    public class NavMeshAgentInterface
    {
        public NavMeshAgent agent;
        public Transform target;
        [SerializeField] bool refreshTargetDestination = true;
        public Action<bool> onSetAtTarget;  // * Set in 2 places = redundant?
    
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


