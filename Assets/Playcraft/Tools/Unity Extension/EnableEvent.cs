using UnityEngine;
using UnityEngine.Events;

public class EnableEvent : MonoBehaviour
{
    [SerializeField] UnityEvent Output;

    void OnEnable()
    {
        Output.Invoke();
    }
}
