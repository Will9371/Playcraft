using UnityEngine;

namespace Playcraft
{
    public class LerpVignetteInOut : MonoBehaviour
    {
        [SerializeField] AccessVignette vignette;
        [SerializeField] LerpFloatInOut inOut;
        
        public void Begin(float duration, Color color) { vignette.color = color; Begin(duration); }
        public void Begin(float duration) { inOut.Begin(this, duration); }
        public void Begin() { inOut.Begin(this); }
        
        void Update() { vignette.intensity = inOut.value; }
        void OnValidate() { inOut.Validate(); }
    }
}