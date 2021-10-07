/*
using System;
using UnityEngine;
using UnityEngine.Events;
using Playcraft;

public class RemappableKeyboardInput : MonoBehaviour
{
    [SerializeField] InputMapping map;
    [SerializeField] Binding[] bindings;
    
    KeyCode[] _keys;

    void Update()
    {
        foreach (var binding in bindings)
        {
            _keys = map.GetKeys(binding.actionId);
            if (_keys == null) continue;
            
            foreach (var key in _keys)
                if (Input.GetKeyDown(key))
                    binding.response.Invoke();
        }
    }
    
    [Serializable]
    public struct Binding
    {
        public SO actionId;
        public UnityEvent response;
    }
}
*/
