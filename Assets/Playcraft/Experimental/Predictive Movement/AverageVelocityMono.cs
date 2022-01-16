using UnityEngine;

namespace Playcraft
{
    public class AverageVelocityMono : MonoBehaviour
    {
        [SerializeField] AverageVelocity process;
        
        public Vector3 averageVelocity => process.averageDelta;
        public float averageMagnitude => process.averageMagnitude;
        public Vector3 averageDirection => process.averageDirection;
        public Vector3 projectedPosition => process.projectedPosition;
        public float edgeAlignment => process.EdgeAlignment(transform.forward);

        void FixedUpdate() { process.FixedUpdate(transform.position); }
    }
}