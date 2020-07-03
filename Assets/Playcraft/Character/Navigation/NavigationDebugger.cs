using UnityEngine;
using UnityEngine.AI;

namespace Playcraft.Navigation
{
    public class NavigationDebugger : MonoBehaviour 
    {
        NavMeshAgent agent;
        LineRenderer line;
        
        private void Awake()
        {
            line = GetComponent<LineRenderer>();
            agent = GetComponent<NavMeshAgent>();
        }
        
        private void Update()
        {
            line.enabled = agent.hasPath;
        
            if (agent.hasPath)
            {
                var corners = agent.path.corners;
                line.positionCount = corners.Length;
                line.SetPositions(corners);
            }
        }
    }
}
