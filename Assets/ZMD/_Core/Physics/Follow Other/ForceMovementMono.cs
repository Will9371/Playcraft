using UnityEngine;

namespace ZMD
{
    /// Applies ForceMove as a standalone component
    public class ForceMovementMono : MonoBehaviour
    {
        [SerializeField] ForceMovement process;
        void FixedUpdate() { process.FixedUpdate(); }
    }
}
