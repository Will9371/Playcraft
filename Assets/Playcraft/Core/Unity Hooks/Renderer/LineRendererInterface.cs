using UnityEngine;

namespace Playcraft
{
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
