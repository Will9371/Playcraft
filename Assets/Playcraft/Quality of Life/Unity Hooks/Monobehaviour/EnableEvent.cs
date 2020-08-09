using UnityEngine;
using UnityEngine.Events;

public class EnableEvent : MonoBehaviour
{
    [SerializeField] UnityEvent Output = default;
    void OnEnable() { Output.Invoke(); }
}
