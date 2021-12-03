using UnityEngine;

namespace Playcraft
{
    /// Applies RotationPID as a standalone component
    public class RotationPIDMono : MonoBehaviour
    {
        [SerializeField] RotationPID process;
        void FixedUpdate() { process.FixedUpdate(); }
    }
}