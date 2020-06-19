using UnityEngine;

namespace Playcraft
{
    public class FloatEventAccess : MonoBehaviour
    {
        [SerializeField] FloatGameEvent invoker = default;
        public void Input(int value) { invoker.Raise(value); }
    }
}
