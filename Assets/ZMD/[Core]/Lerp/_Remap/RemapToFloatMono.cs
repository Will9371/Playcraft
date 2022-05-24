using UnityEngine;

namespace ZMD
{
    public class RemapToFloatMono : MonoBehaviour
    {
        [SerializeField] RemapToFloat remap;
        [SerializeField] FloatEvent output;
        
        public void Input(float value) { output.Invoke(remap.Remap(value)); }
    }
}
