using UnityEngine;

namespace Playcraft
{
    public class RemapToFloatMono : MonoBehaviour
    {
        [SerializeField] RemapToFloat remap;
        [SerializeField] FloatEvent output;
        
        public void Input(float value) { output.Invoke(remap.Remap(value)); }
    }
}
