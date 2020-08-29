using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class UpdateEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnUpdate = default;
        void Update() { OnUpdate.Invoke(); }
    }
}
