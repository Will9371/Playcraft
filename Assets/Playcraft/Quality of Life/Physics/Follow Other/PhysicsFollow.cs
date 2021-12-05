using UnityEngine;

namespace Playcraft
{
    /// Follow a target by applying physical forces.  Allows for more reliable collision detection than ConfigurableJoints.
    /// WARNING: does not account for y-axis rotation.  Set Rigidbody.freezeRotation.y to True to prevent unwanted roll.
    public class PhysicsFollow : MonoBehaviour
    {
        [SerializeField] Rigidbody self;
        [SerializeField] Transform target;
        [SerializeField] PositionPID position;
        [SerializeField] TorqueRotation rotation;
        
        void OnValidate()
        {
            position.rb = self;
            rotation.rb = self;
            rotation.target = target;
        }

        void FixedUpdate()
        {
            rotation.FixedUpdate();
            position.targetPosition = target.position;
            position.FixedUpdate();
        }
    }
}
