using UnityEngine;

namespace Playcraft.Navigation
{
    public class NavMeshAgentInterfaceMono : MonoBehaviour
    {
        [SerializeField] NavMeshAgentInterface process;
        public void SetTarget(Transform value) { process.SetTargetAndDestination(value); }
        void Update() { process.Update(); }
        
        // TBD: consider adding UnityEvent hook to process.OnReachTarget
    }
}
