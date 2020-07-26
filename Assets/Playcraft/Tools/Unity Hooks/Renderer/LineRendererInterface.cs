using UnityEngine;

namespace Playcraft
{
    public class LineRendererInterface : MonoBehaviour
    {
        [SerializeField] LineRenderer line = default;
        [SerializeField] string shaderColorId = default;
        
        public void SetPositionsAndCount(Vector3[] positions)
        {
            line.positionCount = positions.Length;
            line.SetPositions(positions);
        }
        
        public void SetLineColor(ColorSO color) { SetLineColor(color.value); } 
        public void SetLineColor(Color color) 
        { 
            line.startColor = color;
            line.endColor = color;
        }
        
        public void SetMaterialColor(ColorSO color) { SetMaterialColor(color.value); }
        public void SetMaterialColor(Color color) { line.material.SetColor(shaderColorId, color); }
    }
}
