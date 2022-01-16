using UnityEngine;

namespace Playcraft
{
    public class IndicatePredictiveMovement : MonoBehaviour
    {
        [SerializeField] AverageVelocitySO data;
        [SerializeField] Transform velocityIndicator;
        //[SerializeField] Transform accelerationIndicator;
        
        //AverageAcceleration process = new AverageAcceleration();
        AverageVelocity process = new AverageVelocity();

        void OnValidate() { if (data) process.SetData(data); }

        void FixedUpdate() 
        { 
            process.FixedUpdate(transform.position);
            velocityIndicator.position = process.projectedPosition;
            
            //velocityIndicator.position = process.velocity.projectedPosition;
            //accelerationIndicator.position = process.acceleration.projectedPosition;
        }
    }
}
