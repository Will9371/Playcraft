using UnityEngine;

namespace Playcraft
{
    public class GetVelocity : MonoBehaviour
    {
        [SerializeField] int smoothing = 5;
        
        MovingAverageVector3 averageVelocity;
        MovingAverageVector3 averageAngularVelocity;
        
        public Vector3 Velocity => averageVelocity.value;
        public Vector3 AngularVelocity => averageAngularVelocity.value;
        
        Vector3 deltaPosition => transform.position - priorPosition;
        Vector3 velocity => deltaPosition / Time.fixedDeltaTime;
        Quaternion deltaRotation => transform.rotation * Quaternion.Inverse(priorRotation);
        Vector3 angularVelocity => angle * Mathf.Deg2Rad * (1f/Time.fixedDeltaTime) * axis;
        
        Vector3 priorPosition;
        Quaternion priorRotation;
        float angle;
        Vector3 axis;
        
        void Start()
        {
            averageVelocity = new MovingAverageVector3(smoothing);
            averageAngularVelocity = new MovingAverageVector3(smoothing);
        }
        
        void FixedUpdate()
        {
            averageVelocity.Update(velocity);
            
            deltaRotation.ToAngleAxis(out angle, out axis);
            averageAngularVelocity.Update(angularVelocity);
            
            priorPosition = transform.position;
            priorRotation = transform.rotation;        
        }
    }
}