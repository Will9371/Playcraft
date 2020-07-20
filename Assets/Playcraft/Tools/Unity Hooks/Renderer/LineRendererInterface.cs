using Playcraft;
using UnityEngine;

public class LineRendererInterface : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] string shaderColorId;
    
    public void SetPositionsAndCount(Vector3[] positions)
    {
        line.positionCount = positions.Length;
        line.SetPositions(positions);
    }
    
    public void SetLineColor(ColorSO color) { SetLineColor(color.Value); } 
    public void SetLineColor(Color color) 
    { 
        line.startColor = color;
        line.endColor = color;
    }
    
    public void SetMaterialColor(ColorSO color) { SetMaterialColor(color.Value); }
    public void SetMaterialColor(Color color) { line.material.SetColor(shaderColorId, color); }
}
