using UnityEngine;

namespace ZMD
{
    public class IntEventAccess : MonoBehaviour
    {
        [SerializeField] IntGameEvent invoker = default;
        public void Input(int value) { invoker.Raise(value); }
    }
}
