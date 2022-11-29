using UnityEngine;

namespace ZMD
{
    public class LineRendererInterfaceMono : MonoBehaviour
    {
        [SerializeField] new LineRenderer renderer;
        [SerializeField] string shaderColorId;
        
        LineRendererInterface line;
        void Awake() { line = new LineRendererInterface(renderer, shaderColorId); }
        
        public void SetPositionsAndCount(Vector3[] positions) { line.SetPositionsAndCount(positions); }
        
        public void SetLineColor(ColorSO color) { SetLineColor(color.value); } 
        public void SetLineColor(Color color) { line.SetLineColor(color); }
        
        public void SetMaterialColor(ColorSO color) { SetMaterialColor(color.value); }
        public void SetMaterialColor(Color color) { line.SetMaterialColor(color); }
    }
    
    public class LineRendererInterface
    {
        LineRenderer line;
        string shaderColorId;
        
        public LineRendererInterface(LineRenderer line, string shaderColorId)
        {
            this.line = line;
            this.shaderColorId = shaderColorId;
        }
            
        public void SetPositionsAndCount(Vector3[] positions)
        {
            line.positionCount = positions.Length;
            line.SetPositions(positions);
        }
            
        public void SetLineColor(Color color) 
        { 
            line.startColor = color;
            line.endColor = color;
        }
            
        public void SetMaterialColor(Color color) { line.material.SetColor(shaderColorId, color); }
    }
}
