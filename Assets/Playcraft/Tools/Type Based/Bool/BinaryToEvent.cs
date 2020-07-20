using UnityEngine;
using UnityEngine.Events;

public class BinaryToEvent : MonoBehaviour
{
    [SerializeField] UnityEvent OutputTrue;
    [SerializeField] UnityEvent OutputFalse;

    public void Input(bool value)
    {
        if (value) OutputTrue.Invoke();
        else OutputFalse.Invoke();
    }
}
