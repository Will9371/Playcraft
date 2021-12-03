using UnityEngine;

namespace Playcraft
{
    // PID-based system for following a target's position and rotation
    public class FollowPID : MonoBehaviour
    {
        [SerializeField] LocationPID process;
        [SerializeField] Transform target;
        
        void FixedUpdate() 
        {
            process.position.targetPosition = target.position;
            process.rotation.targetAngle = target.eulerAngles; 
            process.FixedUpdate(); 
        }
        
        void OnValidate() { process.OnValidate(); }
    }
}
