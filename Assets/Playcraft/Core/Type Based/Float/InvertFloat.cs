using UnityEngine;

namespace Playcraft
{
    public class InvertFloat : MonoBehaviour
    {
        [SerializeField] FloatEvent Output;
        public void Input(float value) { Input(value, 1f); }
        public void Input(float value, float max) { Output.Invoke(max - value); }
    }
}
