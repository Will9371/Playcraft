using UnityEngine;

namespace Playcraft
{
    public class StateEventAccess : MonoBehaviour
    {
        [SerializeField] StateGameEvent invoker = default;
        public void Input(SO value) { invoker.Raise(value); }
    }
}