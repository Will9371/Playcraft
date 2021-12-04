using UnityEngine;

namespace Playcraft
{
    /// Applies PositionPID as a standalone component
    public class PositionPIDMono : MonoBehaviour
    {
        [SerializeField] PositionPID process;
        void FixedUpdate() { process.FixedUpdate(); }
    }
}
