using UnityEngine;

namespace Playcraft.Navigation
{
    public class RotatingNavMeshAgentMono : MonoBehaviour
    {
        public RotatingNavMeshAgent process;
        
        void OnValidate() { process.OnValidate(); }
        void Start() { process.Start(); }
        void OnDestroy() { process.OnDestroy(); }
        void Update() { process.Update(); }

        void SetAtTarget(bool atTarget) { process.SetAtTarget(atTarget); }
    }
}
