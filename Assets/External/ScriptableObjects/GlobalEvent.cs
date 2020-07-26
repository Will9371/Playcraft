using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Architecture/Global Event")]
public class GlobalEvent : ScriptableObject{

    private List<GlobalEventListener> listeners = new List<GlobalEventListener>();

    void OnDisable()
    {
        listeners.Clear();
    }

    public void Invoke()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void Invoke(GameObject gameObject, float value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(gameObject, value);
    }

    public void Subscribe(GlobalEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void Unsubscribe(GlobalEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

}
