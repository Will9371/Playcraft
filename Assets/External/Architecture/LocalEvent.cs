using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalEvent : MonoBehaviour
{
    private Action action;
    public void Invoke()
    {
        if (action != null)
            action.Invoke();
    }

    public void Subscribe(Action function)
    {
        action += function;
    }

    public void UnSubscribe(Action function)
    {
        action -= function;
    }
}
