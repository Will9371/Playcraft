using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class AwakeEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnAwake = default;
        private void Awake() { OnAwake.Invoke(); }
    }
}
