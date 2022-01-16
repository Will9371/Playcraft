using UnityEngine;

namespace Playcraft
{
    /// Apply TorqueRotation as a standalone component
    public class TorqueRotationMono : MonoBehaviour
    {
        [SerializeField] TorqueRotation process;
        void FixedUpdate() { process.FixedUpdate(); }
    }
}
