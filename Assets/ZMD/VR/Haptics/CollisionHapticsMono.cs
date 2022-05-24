using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace ZMD.VR
{
    public class CollisionHapticsMono : MonoBehaviour
    {
        [SerializeField] CollisionHaptics process;
        void OnCollisionStay(Collision other) { process.OnCollisionStay(other); }
    }

    [Serializable]
    public class CollisionHaptics
    {
        public XRController controller;
        public CollisionHapticsData data;
        
        public void SetData(XRController controller, CollisionHapticsData data)
        {
            this.controller = controller;
            this.data = data;
        }

        public void OnCollisionStay(Collision other)
        {
            if (!controller) return;
            var scaledImpulse = other.impulse.magnitude/data.maxImpulse;
            var amplitude = data.impulseAmplitudeCurve.Evaluate(scaledImpulse);
            controller.SendHapticImpulse(amplitude, .02f);
        }
    }
}

[Serializable]
public struct CollisionHapticsData
{
    public float maxImpulse;  // 20f;
    [Tooltip("Haptics detectable within amplitude range 0.075-1")]
    public AnimationCurve impulseAmplitudeCurve;        
}