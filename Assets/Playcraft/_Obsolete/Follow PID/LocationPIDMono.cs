using UnityEngine;

namespace ZMD
{
    /// Applies LocationPID component to an in-game object
    public class LocationPIDMono : MonoBehaviour
    {
        [SerializeField] LocationPID process;
        void FixedUpdate() { process.FixedUpdate(); }
        void OnValidate() { process.OnValidate(); }
    }
}