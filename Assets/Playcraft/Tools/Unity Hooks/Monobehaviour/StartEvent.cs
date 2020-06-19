using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    [SerializeField] UnityEvent OnStart = default;
    private void Start() { OnStart.Invoke(); }
}
