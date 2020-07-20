using UnityEngine;

namespace Playcraft
{
    public class RendererInterface : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Renderer rend;
        [SerializeField] string shaderColorId;
        #pragma warning restore 0649

        public void SetColor(ColorSO color) { SetColor(color.Value); } 
        public void SetColor(Color color) { rend.material.color = color; }
        
        public void SetMaterialColor(ColorSO color) { SetMaterialColor(color.Value); }
        public void SetMaterialColor(Color color)
        {
            rend.material.SetColor(shaderColorId, color);
        }
    }
}