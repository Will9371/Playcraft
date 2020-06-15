using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] UnityEvent OnEnd;
    
    public void Begin()
    {
        Invoke("End", time);
    }
    
    private void End()
    {
        OnEnd.Invoke();
    }
}
