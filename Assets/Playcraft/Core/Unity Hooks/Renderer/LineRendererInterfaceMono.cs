using UnityEngine;

namespace Playcraft
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
}
