using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Playcraft.VR
{
    public class SimpleHaptics : MonoBehaviour
    {
        [SerializeField] XRController controller;
        [SerializeField] float amplitude = .7f;
        [SerializeField] float duration = .5f;
        
        public void Trigger() { controller.SendHapticImpulse(amplitude, duration); }
    }
}
