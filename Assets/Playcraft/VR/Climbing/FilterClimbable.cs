using UnityEngine;
using UnityEngine.Events;

public class FilterClimbable : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnExit;

    public void InputEntry(Collider collider)
    {
        if (IsClimbable(collider))
            OnEnter.Invoke();
    }
    
    public void InputExit(Collider collider)
    {
        if (IsClimbable(collider))
            OnExit.Invoke();
    }
    
    private bool IsClimbable(Collider collider)
    {
        return collider.GetComponent<Climbable>() != null;
    }
}
