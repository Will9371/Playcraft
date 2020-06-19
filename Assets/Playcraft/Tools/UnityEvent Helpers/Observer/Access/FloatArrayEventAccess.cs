using UnityEngine;

namespace Playcraft
{
    public class FloatArrayEventAccess : MonoBehaviour
    {
        [SerializeField] FloatArrayGameEvent invoker = default;
        public void Input(float[] value) { invoker.Raise(value); }
    }
}