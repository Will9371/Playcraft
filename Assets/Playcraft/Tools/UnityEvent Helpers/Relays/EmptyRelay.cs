using UnityEngine;
using UnityEngine.Events;

public class EmptyRelay : MonoBehaviour
{
    [SerializeField] UnityEvent Output = default;
    
    public void Input()
    {
        Output.Invoke();
    }
}
