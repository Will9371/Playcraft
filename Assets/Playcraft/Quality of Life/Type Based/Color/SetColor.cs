using UnityEngine;

namespace Playcraft
{
    public class SetColor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] new Renderer renderer;
        [SerializeField] new bool enabled = true;
        #pragma warning restore 0649
        
        public void SetEnabled(bool value) { enabled = value; }
        
        public void Input(ColorSO value) { Input(value.value); }
        public void Input(Color value) 
        { 
            if (!enabled) return;
            renderer.material.color = value; 
        }
    }
}
