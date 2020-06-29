using UnityEngine;

namespace Playcraft
{
    public class StateEventAccess : MonoBehaviour
    {
        [SerializeField] StateGameEvent invoker = default;
        public void Input(TagSO value) { invoker.Raise(value); }
    }
}
