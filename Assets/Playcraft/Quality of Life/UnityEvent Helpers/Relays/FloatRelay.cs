using UnityEngine;

namespace Playcraft
{
    public class FloatRelay : MonoBehaviour
    {
        [SerializeField] FloatEvent Output;
        public void Input(float value) { Output.Invoke(value); }
    }
}
