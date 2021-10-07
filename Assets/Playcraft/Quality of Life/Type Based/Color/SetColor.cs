using UnityEngine;

namespace Playcraft
{
    public class SetColor : MonoBehaviour
    {
        [SerializeField] new Renderer renderer;
        [SerializeField] new bool enabled = true;
        
        public void SetEnabled(bool value) { enabled = value; }
        
        public void Input(ColorSO value) { Input(value.value); }
        public void Input(Color value) 
        { 
            if (!enabled) return;
            renderer.material.color = value; 
        }
    }
}
