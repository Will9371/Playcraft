using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace Playcraft.VR
{
    public class CollisionHaptics : MonoBehaviour
    {
        [SerializeField] XRController controller;
        [SerializeField] float maxImpulse = 20f;
        [Tooltip("Haptics detectable within amplitude range 0.075-1")]
        [SerializeField] AnimationCurve impulseAmplitudeCurve;

        void OnCollisionStay(Collision other)
        {
            if (!controller) return;
            var scaledImpulse = other.impulse.magnitude/maxImpulse;
            var amplitude = impulseAmplitudeCurve.Evaluate(scaledImpulse);
            controller.SendHapticImpulse(amplitude, .02f);
        }
    }
}