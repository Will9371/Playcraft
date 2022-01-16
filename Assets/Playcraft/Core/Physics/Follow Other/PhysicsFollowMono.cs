using UnityEngine;

namespace Playcraft
{
    /// Follow a target by applying physical forces.  
    /// Allows for more reliable rotational collision detection than Configurable Joints
    /// and more reliable translational collision detection than Articulation Bodies.
    public class PhysicsFollowMono : MonoBehaviour
    {
        [SerializeField] PhysicsFollow process;
        void OnValidate() { process.OnValidate(); }
        void Start() { process.Start(); }
        void FixedUpdate() { process.FixedUpdate(); }
    }
}
