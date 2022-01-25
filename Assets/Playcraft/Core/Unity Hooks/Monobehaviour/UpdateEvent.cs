using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class UpdateEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnUpdate = default;
        void Update() { OnUpdate.Invoke(); }
    }
}
