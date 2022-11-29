using UnityEngine;

namespace ZMD
{
    public class RendererInterface : MonoBehaviour
    {
        [SerializeField] Renderer rend;
        [SerializeField] string shaderColorId = "_Color";

        public void SetColor(ColorSO color) => SetColor(color.value); 
        public void SetColor(Color color) => rend.material.color = color; 
        
        public void SetMaterialColor(ColorSO color) => SetMaterialColor(color.value); 
        public void SetMaterialColor(Color color) => rend.material.SetColor(shaderColorId, color);
         
        public void SetVisible(bool value) => rend.enabled = value;
    }
}