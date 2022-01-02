using UnityEngine;

namespace Playcraft
{
    public class TestPredictiveMovement : MonoBehaviour
    {
        [SerializeField] AverageVelocityMono measurement;
        void Update() { transform.position = measurement.projectedPosition; }
    }
}
