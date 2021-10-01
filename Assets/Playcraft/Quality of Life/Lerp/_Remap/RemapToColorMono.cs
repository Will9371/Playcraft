using UnityEngine;

namespace Playcraft
{
    public class RemapToColorMono : MonoBehaviour
    {
        [SerializeField] RemapToColor remap;
        [SerializeField] ColorEvent output;
        public void Input(float value) { output.Invoke(remap.Remap(value)); }
    }
}
