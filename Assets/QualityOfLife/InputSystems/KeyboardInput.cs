using System;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{    
    #pragma warning disable 0649
    [SerializeField] Keybinding[] bindings;
    #pragma warning restore 0649
    
    void Update()
    {
        foreach (var binding in bindings)
            binding.Update();
    }
}

[Serializable]
public class Keybinding
{
    #pragma warning disable 0649
    [SerializeField] KeyCode[] keys;
    [SerializeField] bool pressDown, press, pressUp;
    [SerializeField] UnityEvent OnActive;
    #pragma warning restore 0649
    
    public void Update()
    {
        foreach (var key in keys)
            if (pressDown && Input.GetKeyDown(key) ||
                pressUp && Input.GetKeyUp(key) ||
                press && Input.GetKey(key))
                OnActive.Invoke();
    }
}