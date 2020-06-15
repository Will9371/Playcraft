using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] float time;
    [SerializeField] UnityEvent OnEnd;
    #pragma warning restore 0649
    
    public void Begin()
    {
        Invoke("End", time);
    }
    
    private void End()
    {
        OnEnd.Invoke();
    }
}
