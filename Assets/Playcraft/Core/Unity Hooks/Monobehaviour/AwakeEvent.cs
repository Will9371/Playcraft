using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class AwakeEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnAwake = default;
        private void Awake() { OnAwake.Invoke(); }
    }
}
