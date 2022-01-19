using UnityEngine;

namespace Playcraft
{
    // DEPRECATE: merge with AverageVelocityMono
    public class IndicatePredictiveMovement : MonoBehaviour
    {
        [SerializeField] AverageVelocitySO data;
        [SerializeField] Transform velocityIndicator;

        AverageVelocity process = new AverageVelocity();

        void OnValidate() { if (data) process.SetData(data); }

        void FixedUpdate() 
        { 
            process.FixedUpdate(transform.position);
            velocityIndicator.position = process.projectedPosition;
        }
    }
}
