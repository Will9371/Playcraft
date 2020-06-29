using UnityEngine;

namespace Playcraft
{
    public class FloatEventAccess : MonoBehaviour
    {
        [SerializeField] FloatGameEvent invoker = default;
        public void Input(float value) { invoker.Raise(value); }
    }
}
