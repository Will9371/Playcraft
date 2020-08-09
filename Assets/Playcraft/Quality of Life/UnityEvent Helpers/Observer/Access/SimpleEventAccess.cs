using UnityEngine;

namespace Playcraft
{
    public class SimpleEventAccess : MonoBehaviour
    {
        [SerializeField] SimpleGameEvent invoker = default;
        public void Input() { invoker.Raise(); }
    }
}
