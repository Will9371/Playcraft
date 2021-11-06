using UnityEngine;
using UnityEngine.UI;

namespace Playcraft
{
    public class ScreenFadeInOut : MonoBehaviour
    {
        [SerializeField] Image fade;
        [SerializeField] LerpFloatInOut inOut;
        
        Color color => fade.color;
        
        public void Begin() { inOut.Begin(this); }
        void Update() { fade.color = new Color(color.r, color.g, color.b, inOut.value); }
        void OnEnable() { fade.enabled = true; }
        void OnDisable() { fade.enabled = false; }
        void OnValidate() { inOut.Validate(); }
    }
}
