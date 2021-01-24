using UnityEngine;
using UnityEngine.Events;

public class Relay : MonoBehaviour
{
    [SerializeField] UnityEvent Output = default;
    public void Input() { Output.Invoke(); }
}
