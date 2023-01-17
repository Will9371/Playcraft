using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class ClickEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent onMouseDown;
        void OnMouseDown() => onMouseDown.Invoke();
    }
}
