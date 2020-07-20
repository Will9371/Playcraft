using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class TrinaryEvent : UnityEvent<Trinary> { }
public enum Trinary { True, False, Unknown }

public class TrinaryToEvent : MonoBehaviour
{
    [SerializeField] TrinaryEventData[] actions;

    public void Input(Trinary value)
    {
        foreach (var element in actions)
            if (element.value == value)
                element.OnActivate.Invoke();
    }
}

[Serializable]
public struct TrinaryEventData
{
    public Trinary value;
    public UnityEvent OnActivate;
}

