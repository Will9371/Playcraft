using System;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{    
    [SerializeField] Keybinding[] bindings;
    
    void Update()
    {
        foreach (var binding in bindings)
            binding.Update();
    }
}

[Serializable]
public class Keybinding
{
    [SerializeField] KeyCode[] keys;
    [SerializeField] bool pressDown, press, pressUp;
    [SerializeField] UnityEvent OnActive;
    
    public void Update()
    {
        foreach (var key in keys)
            if (pressDown && Input.GetKeyDown(key) ||
                pressUp && Input.GetKeyUp(key) ||
                press && Input.GetKey(key))
                OnActive.Invoke();
    }
}