using UnityEngine;

namespace Playcraft
{
    public class AccumulateVelocity : MonoBehaviour
    {
        [SerializeField] AverageVelocity runningAverage;
        
        public Vector3 averageVelocity => runningAverage.averageDelta;
        public float averageMagnitude => averageVelocity.magnitude;
        public Vector3 averageDirection => averageVelocity.normalized;
        public float edgeAlignment => Mathf.Abs(Vector3.Dot(transform.forward, averageDirection));
        
        void Start() { runningAverage.Validate(); }
        void OnValidate() { runningAverage.Validate(); }
        void FixedUpdate() { runningAverage.Update(transform.position); }
    }
}
