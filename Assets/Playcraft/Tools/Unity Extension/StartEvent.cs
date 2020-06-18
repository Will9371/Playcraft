using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    [SerializeField] UnityEvent OnStart;

    private void Start()
    {
        OnStart.Invoke();
    }
}
