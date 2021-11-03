using UnityEngine;

namespace Playcraft
{
    public class TestPredictiveMovement : MonoBehaviour
    {
        [SerializeField] AccumulateVelocity measurement;
        void Update() { transform.position = measurement.projectedPosition; }
    }
}
