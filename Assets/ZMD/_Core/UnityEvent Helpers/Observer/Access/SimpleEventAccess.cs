using UnityEngine;

namespace ZMD
{
    public class SimpleEventAccess : MonoBehaviour
    {
        [SerializeField] SimpleGameEvent invoker = default;
        public void Input() { invoker.Raise(); }
    }
}
