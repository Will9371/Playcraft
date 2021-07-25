using UnityEngine;
using UnityEngine.Events;

public class Relay : MonoBehaviour
{
    [SerializeField] UnityEvent Output;
    public void Input() { Output.Invoke(); }
}
