using UnityEngine;

namespace Playcraft
{
    /// PID-based system for following a target's position and rotation
    /// ERROR: erratic results in RotationPID (PositionPID is OK), use PhysicsFollow instead
    public class FollowPID : MonoBehaviour
    {
        [SerializeField] LocationPID process;
        [SerializeField] Transform target;
        [SerializeField] bool applyPosition = true;
        [SerializeField] bool applyRotation = true;
        
        void FixedUpdate() 
        {
            if (applyPosition) process.position.targetPosition = target.position;
            if (applyRotation) process.rotation.targetAngle = target.eulerAngles; 
            process.FixedUpdate(); 
        }
        
        void OnValidate() { process.OnValidate(); }
    }
}
